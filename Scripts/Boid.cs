using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
	public SelectedBoidSO TargetBoid;
	public BoidListSO Boids;

	public BoidConstantSO AccelerationMaxSO;
	public BoidConstantSO AccuracySO;
	public BoidConstantSO AlignmentConstantSO;
	public BoidConstantSO BloodthirstConstantSO;
	public BoidConstantSO CohesionConstantSO;
	public BoidConstantSO CooldownSO;
	public BoidConstantSO DamageSO;
	public BoidConstantSO FearSO;
	public BoidConstantSO MaxChaseDistanceSO;
	public BoidConstantSO MaxFireDistanceSO;
	public BoidConstantSO MaxHealthSO;
	public BoidConstantSO NeighborhoodRadiusSO;
	public BoidConstantSO TargetAcqMaxAngleSO;
	public BoidConstantSO TargetAcqMinFitnessSO;
	public BoidConstantSO TargetAcqRadiusSO;
	public BoidConstantSO VelocityMaxSO;

	public FloatVariableSO BoundaryConstant;
	public FloatVariableSO BoundaryRadius;
	public FloatVariableSO ChaseConstant;
	public FloatVariableSO EnemyAlignmentConstant;
	public FloatVariableSO EvadeConstant;
	public FloatVariableSO MaxFireAngle;
	public FloatVariableSO SeparationConstant;
	public FloatVariableSO TargetAcqAngleMod;
	public FloatVariableSO TargetFitnessBonus;

	private Vector3 velocity = Vector3.zero;
	public GameObject explosionPrefab;
	public GameObject laserPrefab;
	public BoidInfoPanel boidInfoPanel;
	private Goal goal;
	public PilotSO pilot;
	public ShipSO ship;

	// Burst
	private int burstTimer = 0;
	private int burstDuration = 30;
	private int burstCooldown = 240;
	private int burstCooldownTimer = 0;
	private Vector3 burstVector;

	public bool isEnemy = false;
	public Boid target;
	public Boid pursuer;

	// Internal
	private int cooldownTimer = 0;
	public float currentHealth = 100;
	private BoidState state = BoidState.Flock;

	#region Attribute-Based Constants
	public float AccelerationMax() { return AccelerationMaxSO.GetValue(ship.acceleration - (ship.armor / 2)); }
	public float Accuracy() { return AccuracySO.GetValue(pilot.skill); }
	public float AlignmentConstant() { return AlignmentConstantSO.GetValue(pilot.ego); }
	public float BloodthirstConstant() { return BloodthirstConstantSO.GetValue(pilot.ego); }
	public float CohesionConstant() { return CohesionConstantSO.GetValue(pilot.sociability); }
	public int 	 Cooldown() { return (int)CooldownSO.GetValue(ship.damage); }
	public float Damage() { return DamageSO.GetValue(ship.damage); }
	public float Fear() { return FearSO.GetValue(pilot.ego); }
	public float MaxChaseDistance() { return MaxChaseDistanceSO.GetValue(pilot.persistence); }
	public float MaxFireDistance() { return MaxFireDistanceSO.GetValue(ship.range); }
	public float MaxHealth() { return ship == null ? 0 : MaxHealthSO.GetValue(ship.armor); }
	public float NeighborhoodRadius() { return NeighborhoodRadiusSO.GetValue(ship.sensors); }
	public float TargetAcqMaxAngle() { return TargetAcqMaxAngleSO.GetValue(pilot.vision); }
	public float TargetAcqMinFitness() { return TargetAcqMinFitnessSO.GetValue(pilot.persistence); }
	public float TargetAcqRadius() { return TargetAcqRadiusSO.GetValue(ship.sensors); }
	public float VelocityMax() { return VelocityMaxSO.GetValue(ship.topSpeed); }
	#endregion

    void Awake()
    {
		goal = FindObjectOfType<Goal>();
    }

    // Update is called once per frame
    void Update()
    {
		if (ship == null || pilot == null) return;

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

	public void Initialize(PilotSO pilot, ShipSO ship)
	{
		this.pilot = pilot;
		this.ship = ship;

		float scale = (MaxHealth() / 500) + 1;
		transform.localScale = new Vector3(scale, scale, scale);

		this.currentHealth = MaxHealth();

		Boids.RegisterBoid(this);
	}

	void LimitVelocity()
	{
		velocity = Vector3.ClampMagnitude(velocity, VelocityMax());
	}

	void UpdateState()
	{
		switch(state) {
			case BoidState.Flock:
			{
				Boid fittestTarget = null;
				float fittestValue = TargetAcqMinFitness();

				foreach(Boid boid in Boids.Value) {
					if (boid != null && boid.isEnemy != isEnemy) {
						Vector3 displacement = boid.gameObject.transform.position - transform.position;

						if (displacement.sqrMagnitude > TargetAcqRadius() * TargetAcqRadius()) {
							continue;
						}

						float angleValue = ((TargetAcqMaxAngle() - Vector3.Angle(velocity, displacement)) / TargetAcqMaxAngle()) * TargetAcqAngleMod.Value;
						float distValue = (displacement.magnitude / TargetAcqRadius()) * (1 - TargetAcqAngleMod.Value);

						float targetValue = 0.0f;
						if (!isEnemy && TargetBoid.Boid != this) {
							if (boid == TargetBoid.Boid || boid.target == TargetBoid.Boid) {
								targetValue += TargetFitnessBonus.Value;
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
				float maxChaseDistance = MaxChaseDistance();
				if (displacement.sqrMagnitude > maxChaseDistance * maxChaseDistance) {
					target = null;
					state = BoidState.Flock;
					break;
				}

				if (Vector3.Angle(velocity, displacement) < MaxFireAngle.Value && cooldownTimer < 0) {
					cooldownTimer = Cooldown();
					target.Fire(this, Accuracy(), Damage());
				}
				break;
			}
			case BoidState.Evade:
			{
				if (pursuer == null) {
					state = BoidState.Flock;
					break;
				}

				float enemyMaxChaseDistance = pursuer.MaxChaseDistance();
				if (Displacement(pursuer).sqrMagnitude > enemyMaxChaseDistance * enemyMaxChaseDistance) {
					pursuer = null;
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
		// 	burstVector = Vector3.ClampMagnitude(deltaVelocity, VelocityMax() * 1.5f);
		// 	burstCooldownTimer = burstCooldown;
		// 	burstTimer = burstDuration;
		// 	velocity = Vector3.zero;
		// }

		deltaVelocity += perpendicularRatio * -velocity; // If the boid wants to move in a perpendicular direction, it has to slow down first.
		velocity += Vector3.ClampMagnitude(deltaVelocity, AccelerationMax());
	}

	Vector3 Bloodthirst()
	{
		Vector3 perceivedCenter = Vector3.zero;
		int count = 0;
		foreach(Boid boid in Boids.Value) {
			if (boid != null && boid.isEnemy != isEnemy) {
				count++;
				perceivedCenter += boid.gameObject.transform.position;
			}
		}

		if (count == 0) {
			return Vector3.zero;
		}

		perceivedCenter = perceivedCenter / count;
		return (perceivedCenter - transform.position) / BloodthirstConstant();
	}

	Vector3 Separation()
	{
		Vector3 separation = Vector3.zero;
		foreach(Boid boid in Boids.Value) {
			if (boid != null && boid != this) {
				Vector3 displacement = Displacement(boid);
				if (displacement.magnitude < NeighborhoodRadius()) {
					separation -= displacement;
				}
			}
		}
		return separation / SeparationConstant.Value;
	}

	Vector3 Alignment()
	{
		Vector3 perceivedVelocity = Vector3.zero;
		int count = 0;
		foreach(Boid boid in Boids.Value) {
			if (boid != null && boid != this && boid.isEnemy == isEnemy) {
				count++;
				perceivedVelocity += boid.velocity;
			}
		}

		if (count == 0) {
			return Vector3.zero;
		}

		perceivedVelocity = perceivedVelocity / count;
		return (perceivedVelocity - velocity) / AlignmentConstant();
	}

	Vector3 Cohesion()
	{
		Vector3 perceivedCenter = Vector3.zero;
		int count = 0;
		foreach(Boid boid in Boids.Value) {
			if (boid != null && boid != this && boid.isEnemy == isEnemy) {
				count++;
				perceivedCenter += boid.gameObject.transform.position;
			}
		}

		if (count == 0) {
			return Vector3.zero;
		}

		perceivedCenter = perceivedCenter / count;
		return (perceivedCenter - transform.position) / CohesionConstant();
	}


	// Chase
	Vector3 Chase()
	{
		if (target != null) {
			float trailDistance = MaxFireDistance() * 0.75f;
			Vector3 behindTarget = target.gameObject.transform.position - (target.velocity.normalized * trailDistance);
			return (behindTarget - transform.position) / ChaseConstant.Value;
		}
		return Vector3.zero;
	}

	Vector3 AlignWithTarget()
	{
		if (target != null) {
			return (target.velocity - velocity) / EnemyAlignmentConstant.Value;
		}
		return Vector3.zero;
	}


	// Evade
	Vector3 Evade()
	{
		if (pursuer != null) {
			return -Displacement(pursuer) / EvadeConstant.Value;
		}
		return Vector3.zero;
	}

	Vector3 EvadeLineOfSight()
	{
		if (pursuer != null) {
			Vector3 projection = Vector3.Project(-Displacement(pursuer), pursuer.velocity);
			return (transform.position - projection) / EvadeConstant.Value;
		}
		return Vector3.zero;
	}


	void Fire(Boid firedBy, float accuracy, float damage)
	{
		Vector3 rayDir;
		Vector3 displacement = Displacement(firedBy);
		float distanceMod = 1 - (displacement.magnitude / MaxFireDistance());
		float precision = Random.value - (accuracy * distanceMod);
//		Debug.Log("Precision: " + precision + ";  Accuracy: " + accuracy + "; DistanceMod: " + distanceMod);
		if (precision < 0) {
			rayDir = -displacement;
			currentHealth -= damage;
			if (currentHealth <= 0) {
				Die();
			}
		} else {
			rayDir = firedBy.velocity * 1000;
		}
		if (state == BoidState.Flock || (currentHealth / MaxHealth()) + precision < Fear()) {
			pursuer = firedBy;
			state = BoidState.Evade;
		}

		CreateLine(
			firedBy.gameObject.transform.position,
			rayDir,
			firedBy.isEnemy ? Color.red : Color.white,
			damage
		);
	}

	void Die()
	{
		gameObject.AddComponent<BoidDeath>();
		BoidDeath bd = GetComponent<BoidDeath>();
		LimitVelocity();
		bd.Velocity = velocity;
		bd.ExplosionPrefab = explosionPrefab;

		Boids.UnregisterBoid(this);
		Destroy(this);
	}

	Vector3 Goal()
	{
		Vector3 displacement = goal.gameObject.transform.position - transform.position;
		if (displacement.sqrMagnitude > BoundaryRadius.Value * BoundaryRadius.Value) {
			return displacement / BoundaryConstant.Value;
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
