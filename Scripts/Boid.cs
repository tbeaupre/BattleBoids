using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
	public Vector3 velocity = Vector3.zero;
	public Boid[] boids;
	private Goal goal;

	public float neighborhoodRadius = 10.0f;
	public float velocityMax = 1.0f;
	public float cohesionConstant = 100;
	public float alignmentConstant = 20;
	public float separationConstant = 3;
	public float goalConstant = 1000;
	public float chaseConstant = 100;
	public float evadeConstant = 100;

	// Target Acquisition
	public float targetAcqRadius = 20.0f;
	public float targetAcqAngleMod = 0.7f;
	public float targetAcqMinAngle = 75.0f;
	public float targetAcqMinFitness = 0.3f;

	// Chase
	public float maxChaseDistance = 25.0f;
	public float maxFireAngle = 3.0f;
	public float accuracy = 0.3f;
	public int cooldown = 60; // In frames
	public float damage = 70;

	// Evade
	public float fear = 0.5f;
	public float maxEvadeDistance = 25.0f;

	// Boundaries
	public float boundaryRadius = 100.0f;

	// Internal
	public bool isEnemy = false;
	public BoidState state = BoidState.Flock;
	public Boid target;
	public int cooldownTimer = 0;
	public float maxHealth = 100;
	public float currentHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
		boids = FindObjectsOfType<Boid>();
		goal = FindObjectOfType<Goal>();
    }

    // Update is called once per frame
    void Update()
    {
		cooldownTimer--;
		UpdateState();
		UpdateVelocity();
		LimitVelocity();
		transform.position += velocity;
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

				foreach(Boid boid in boids) {
					if (boid != null && boid.isEnemy != isEnemy) {
						Vector3 displacement = boid.gameObject.transform.position - transform.position;

						if (displacement.sqrMagnitude > targetAcqRadius * targetAcqRadius) {
							continue;
						}

						float angleValue = ((targetAcqMinAngle - Vector3.Angle(velocity, displacement)) / targetAcqMinAngle) * targetAcqAngleMod;
						float distValue = (displacement.magnitude / targetAcqRadius) * (1 - targetAcqAngleMod);
						float fitnessValue = angleValue + distValue;

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
				} else if (Vector3.Angle(velocity, displacement) < maxFireAngle && cooldownTimer < 0) {
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
		velocity += Separation();
		velocity += Goal();

		switch(state) {
			case BoidState.Flock:
			{
				velocity += Alignment();
				velocity += Cohesion();
				break;
			}
			case BoidState.Chase:
			{
				velocity += Chase();
				break;
			}
			case BoidState.Evade:
			{
				velocity += Evade();
				break;
			}
		}
	}

	Vector3 Separation()
	{
		Vector3 separation = Vector3.zero;
		foreach(Boid boid in boids) {
			if (boid != null && boid != this && boid.isEnemy == isEnemy) {
				Vector3 displacement = boid.gameObject.transform.position - transform.position;
				if (displacement.magnitude < neighborhoodRadius) {
					separation = separation - displacement;
				}
			}
		}
		return separation / separationConstant;
	}

	Vector3 Alignment()
	{
		Vector3 perceivedVelocity = Vector3.zero;
		int count = 0;
		foreach(Boid boid in boids) {
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
		foreach(Boid boid in boids) {
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

	Vector3 Chase()
	{
		if (target != null) {
			return (target.gameObject.transform.position - transform.position) / chaseConstant;
		}
		return Vector3.zero;
	}

	Vector3 Evade()
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
		float closeness = Random.value - accuracy;
		Debug.Log("Closeness: " + closeness + ";  Accuracy: " + accuracy);
		if (closeness < 0) {
			rayDir = -Displacement(firedBy);
			currentHealth -= damage;
			if (currentHealth < 0) {
				Destroy(this);
			}
		} else {
			rayDir = firedBy.velocity * 1000;
		}
		if (state == BoidState.Flock || (currentHealth / maxHealth) + closeness < fear) {
			target = firedBy;
			state = BoidState.Evade;
		}
		Debug.DrawRay(
			firedBy.gameObject.transform.position,
			rayDir,
			firedBy.isEnemy ? Color.red : Color.white,
			0.1f
		);

	}

	Vector3 Goal()
	{
		return (goal.gameObject.transform.position - transform.position) / goalConstant;
	}

	private

	Vector3 Displacement(Boid boid)
	{
		return boid.gameObject.transform.position - transform.position;
	}
}
