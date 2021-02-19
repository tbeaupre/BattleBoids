using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
	public SelectedBoidScriptableObject TargetBoid;

	private Vector3 velocity = Vector3.zero;
	public GameObject laserPrefab;
	public BoidInfoPanel boidInfoPanel;
	private Goal goal;
	public PilotScriptableObject pilot;
	public ShipScriptableObject ship;

	private float neighborhoodRadius = 10.0f;
	private float accelerationMax = 0.1f;
	private float velocityMax = 1.0f;
	private float cohesionConstant = 100;
	private float alignmentConstant = 20;
	private float separationConstant = 3;
	private float goalConstant = 100;
	private float chaseConstant = 100;
	private float evadeConstant = 100;
	private float bloodthirstConstant = 100;

	// Target Acquisition
	private float targetAcqRadius = 20.0f;
	private float targetAcqAngleMod = 0.7f;
	private float targetAcqMaxAngle = 75.0f;
	private float targetAcqMinFitness = 0.3f;

	// Chase
	private float maxChaseDistance = 25.0f;
	private float maxFireDistance = 20.0f;
	private float maxFireAngle = 3.0f;
	private float accuracy = 0.3f;
	private int cooldown = 20; // In frames
	private float damage = 70;

	// Evade
	private float fear = 0.5f;
	private float maxEvadeDistance = 25.0f;

	// Boundaries
	private float boundaryRadius = 50.0f;

	// Burst
	private int burstTimer = 0;
	private int burstDuration = 30;
	private int burstCooldown = 240;
	private int burstCooldownTimer = 0;
	private Vector3 burstVector;

	public bool isEnemy = false;
	public BoidState state = BoidState.Flock;
	public Boid target;

	// Internal
	private int cooldownTimer = 0;
	public float maxHealth = 100;
	public float currentHealth = 100;

    void Awake()
    {
		goal = FindObjectOfType<Goal>();
    }

    // Update is called once per frame
    void Update()
    {
		UpdateTimers();
		UpdateState();
		UpdateVelocity();
		LimitVelocity();
		transform.forward = velocity;

		if (burstTimer > 0) {
			transform.position += burstVector / (1 + burstDuration - burstTimer);
			transform.position += velocity / burstTimer;
		} else {
			transform.position += velocity;
		}
    }

	void UpdateTimers()
	{
		cooldownTimer--;
		burstTimer--;
		burstCooldownTimer--;
	}

	public void Initialize(PilotScriptableObject pilot, ShipScriptableObject ship)
	{
		this.neighborhoodRadius = CalcRandProp(10, 5, ship.sensors, false);
		this.accelerationMax = CalcRandProp(0.05f, 0.05f, ship.acceleration - (ship.armor / 2), false);
		this.velocityMax = CalcRandProp(0.7f, 1, ship.topSpeed, false);
		this.maxHealth = CalcRandProp(50, 300, ship.armor, false);
		this.targetAcqRadius = CalcRandProp(15, 10, ship.sensors, false);
		this.maxFireDistance = CalcRandProp(15, 10, ship.range, false);
		this.cooldown = (int)CalcRandProp(20, 70, ship.damage, false);
		this.damage = CalcRandProp(10, 200, ship.damage, false);

		this.bloodthirstConstant = CalcRandProp(150, 60, pilot.ego, true);
		this.cohesionConstant = CalcRandProp(70, 60, pilot.sociability, true);
		this.alignmentConstant = CalcRandProp(10, 20, pilot.ego, false);
		this.targetAcqMaxAngle = CalcRandProp(60, 40, pilot.vision, false);
		this.targetAcqMinFitness = CalcRandProp(0.2f, 0.2f, pilot.persistence, true);
		this.maxChaseDistance = CalcRandProp(targetAcqRadius, 10, pilot.persistence, false);
		this.accuracy = CalcRandProp(0.5f, 0.6f, pilot.skill, false);
		this.fear = CalcRandProp(0, 1, pilot.ego, true);

		float scale = (maxHealth / 500) + 1;
		transform.localScale = new Vector3(scale, scale, scale);

		this.currentHealth = this.maxHealth;

		this.pilot = pilot;
		this.ship = ship;

		BattleManager.Instance.RegisterBoid(this);
	}

	void LimitVelocity()
	{
		velocity = Vector3.ClampMagnitude(velocity, velocityMax);
	}

	void UpdateState()
	{
		switch(state) {
			case BoidState.Flock:
			{
				Boid fittestTarget = null;
				float fittestValue = targetAcqMinFitness;

				foreach(Boid boid in BattleManager.Instance.Boids) {
					if (boid != null && boid.isEnemy != isEnemy) {
						Vector3 displacement = boid.gameObject.transform.position - transform.position;

						if (displacement.sqrMagnitude > targetAcqRadius * targetAcqRadius) {
							continue;
						}

						float angleValue = ((targetAcqMaxAngle - Vector3.Angle(velocity, displacement)) / targetAcqMaxAngle) * targetAcqAngleMod;
						float distValue = (displacement.magnitude / targetAcqRadius) * (1 - targetAcqAngleMod);

						float targetValue = 0.0f;
						if (!isEnemy && TargetBoid.Boid != this) {
							bool isChasingTarget = boid.state == BoidState.Chase && boid.target == TargetBoid.Boid;
							if (boid == TargetBoid.Boid || isChasingTarget) {
								targetValue += 0.3f;
							}
						}

						float fitnessValue = angleValue + distValue + targetValue;

						if (fitnessValue > fittestValue) {
							fittestTarget = boid;
							fittestValue = fitnessValue;
						}
					}
				}

				if (fittestTarget != null) {
					target = fittestTarget;
					state = BoidState.Chase;
				}
				break;
			}
			case BoidState.Chase:
			{
				if (target == null) {
					state = BoidState.Flock;
					break;
				}

				Vector3 displacement = Displacement(target);
				if (displacement.sqrMagnitude > maxChaseDistance * maxChaseDistance) {
					target = null;
					state = BoidState.Flock;
					break;
				}

				if (Vector3.Angle(velocity, displacement) < maxFireAngle && cooldownTimer < 0) {
					cooldownTimer = cooldown;
					target.Fire(this, accuracy, damage);
				}
				break;
			}
			case BoidState.Evade:
			{
				if (target == null) {
					state = BoidState.Flock;
					break;
				}

				if (Displacement(target).sqrMagnitude > maxEvadeDistance * maxEvadeDistance) {
					target = null;
					state = BoidState.Flock;
				}
				break;
			}
		}
	}

	void UpdateVelocity()
	{
		Vector3 deltaVelocity = Vector3.zero;
		deltaVelocity += Separation();
		deltaVelocity += Goal();

		Color color = Color.white;
		switch(state) {
			case BoidState.Flock:
			{
				deltaVelocity += Alignment();
				deltaVelocity += Cohesion();
				deltaVelocity += Bloodthirst();
				color = Color.white;
				break;
			}
			case BoidState.Chase:
			{
				deltaVelocity += Chase();
				deltaVelocity += AlignWithTarget();
				deltaVelocity += Bloodthirst();
				color = Color.red;
				break;
			}
			case BoidState.Evade:
			{
				deltaVelocity += Evade();
				deltaVelocity += EvadeLineOfSight();
				color = Color.blue;
				break;
			}
		}

		Debug.DrawLine(
			transform.position,
			transform.position + (velocity * 5.0f),
			color,
			1f / 90f
		);
		
		float normalizedDot = Vector3.Dot(velocity.normalized, deltaVelocity.normalized); // 1 if same dir, -1 if opposite dirs, and 0 if perpendicular.
		float perpendicularRatio = (1 - Mathf.Abs(normalizedDot)); // 0 if forward or back, 1 if perpendicular.

		// // Burst Logic
		// if (perpendicularRatio > 0.6 && burstCooldownTimer < 0)
		// {
		// 	Debug.Log("Bursting");
		// 	burstVector = Vector3.ClampMagnitude(deltaVelocity, velocityMax * 1.5f);
		// 	burstCooldownTimer = burstCooldown;
		// 	burstTimer = burstDuration;
		// 	velocity = Vector3.zero;
		// }

		deltaVelocity += perpendicularRatio * -velocity; // If the boid wants to move in a perpendicular direction, it has to slow down first.
		velocity += Vector3.ClampMagnitude(deltaVelocity, accelerationMax);
	}

	Vector3 Bloodthirst()
	{
		Vector3 perceivedCenter = Vector3.zero;
		int count = 0;
		foreach(Boid boid in BattleManager.Instance.Boids) {
			if (boid != null && boid.isEnemy != isEnemy) {
				count++;
				perceivedCenter += boid.gameObject.transform.position;
			}
		}

		if (count == 0) {
			return Vector3.zero;
		}

		perceivedCenter = perceivedCenter / count;
		return (perceivedCenter - transform.position) / bloodthirstConstant;
	}

	Vector3 Separation()
	{
		Vector3 separation = Vector3.zero;
		foreach(Boid boid in BattleManager.Instance.Boids) {
			if (boid != null && boid != this) {
				Vector3 displacement = Displacement(boid);
				if (displacement.magnitude < neighborhoodRadius) {
					separation -= displacement;
				}
			}
		}
		return separation / separationConstant;
	}

	Vector3 Alignment()
	{
		Vector3 perceivedVelocity = Vector3.zero;
		int count = 0;
		foreach(Boid boid in BattleManager.Instance.Boids) {
			if (boid != null && boid != this && boid.isEnemy == isEnemy) {
				count++;
				perceivedVelocity += boid.velocity;
			}
		}

		if (count == 0) {
			return Vector3.zero;
		}

		perceivedVelocity = perceivedVelocity / count;
		return (perceivedVelocity - velocity) / alignmentConstant;
	}

	Vector3 Cohesion()
	{
		Vector3 perceivedCenter = Vector3.zero;
		int count = 0;
		foreach(Boid boid in BattleManager.Instance.Boids) {
			if (boid != null && boid != this && boid.isEnemy == isEnemy) {
				count++;
				perceivedCenter += boid.gameObject.transform.position;
			}
		}

		if (count == 0) {
			return Vector3.zero;
		}

		perceivedCenter = perceivedCenter / count;
		return (perceivedCenter - transform.position) / cohesionConstant;
	}


	// Chase
	Vector3 Chase()
	{
		if (target != null) {
			float trailDistance = maxFireDistance * 0.75f;
			Vector3 behindTarget = target.gameObject.transform.position - (target.velocity.normalized * trailDistance);
			return (behindTarget - transform.position) / chaseConstant;
		}
		return Vector3.zero;
	}

	Vector3 AlignWithTarget()
	{
		if (target != null) {
			return (target.velocity - velocity) / alignmentConstant;
		}
		return Vector3.zero;
	}


	// Evade
	Vector3 Evade()
	{
		if (target != null) {
			return -Displacement(target) / evadeConstant;
		}
		return Vector3.zero;
	}

	Vector3 EvadeLineOfSight()
	{
		if (target != null) {
			Vector3 projection = Vector3.Project(-Displacement(target), target.velocity);
			return (transform.position - projection) / evadeConstant;
		}
		return Vector3.zero;
	}


	void Fire(Boid firedBy, float accuracy, float damage)
	{
		Vector3 rayDir;
		Vector3 displacement = Displacement(firedBy);
		float distanceMod = 1 - (displacement.magnitude / maxFireDistance);
		float precision = Random.value - (accuracy * distanceMod);
//		Debug.Log("Precision: " + precision + ";  Accuracy: " + accuracy + "; DistanceMod: " + distanceMod);
		if (precision < 0) {
			rayDir = -displacement;
			currentHealth -= damage;
			if (currentHealth <= 0) {
				BattleManager.Instance.OnBoidDeath(this);
				Destroy(this);
			}
		} else {
			rayDir = firedBy.velocity * 1000;
		}
		if (state == BoidState.Flock || (currentHealth / maxHealth) + precision < fear) {
			target = firedBy;
			state = BoidState.Evade;
		}

		CreateLine(
			firedBy.gameObject.transform.position,
			rayDir,
			firedBy.isEnemy ? Color.red : Color.white,
			firedBy.damage
		);
	}

	Vector3 Goal()
	{
		Vector3 displacement = goal.gameObject.transform.position - transform.position;
		if (displacement.sqrMagnitude > boundaryRadius * boundaryRadius) {
			return displacement / goalConstant;
		}
		return Vector3.zero;
	}

	private

	Vector3 Displacement(Boid boid)
	{
		return boid.gameObject.transform.position - transform.position;
	}

	void CreateLine(Vector3 start, Vector3 rayDir, Color color, float damage)
	{
		GameObject line = Instantiate(laserPrefab);
		LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
		lineRenderer.SetPositions(new Vector3[] { start, start + rayDir });
		lineRenderer.startColor = color;
		lineRenderer.startWidth = damage * 0.005f;
		lineRenderer.endColor = color;
		lineRenderer.endWidth = damage * 0.005f;
	}

	float CalcRandProp(float min, float range, float rand, bool invert)
	{
		float modifier = invert ? (1 - rand) : rand;
		return range * modifier + min;
	}
}
