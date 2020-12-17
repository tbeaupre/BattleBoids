using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoidData
{
	public float neighborhoodRadius;
	public float accelerationMax;
	public float velocityMax;
	public float cohesionConstant;
	public float alignmentConstant;
	public float separationConstant;
	public float chaseConstant;
	public float evadeConstant;
	public float maxHealth;

	// Target Acquisition
	public float targetAcqRadius;
	public float targetAcqAngleMod;
	public float targetAcqMaxAngle;
	public float targetAcqMinFitness;

	// Chase
	public float maxChaseDistance;
	public float maxFireDistance;
	public float maxFireAngle;
	public float accuracy;
	public int cooldown; // In frames
	public float damage;

	// Evade
	public float fear;
	public float maxEvadeDistance;

	public BoidData(Boid boid)
	{
		neighborhoodRadius = boid.neighborhoodRadius;
		accelerationMax = boid.accelerationMax;
		velocityMax = boid.velocityMax;
		cohesionConstant = boid.cohesionConstant;
		alignmentConstant = boid.alignmentConstant;
		separationConstant = boid.separationConstant;
		chaseConstant = boid.chaseConstant;
		evadeConstant = boid.evadeConstant;
		maxHealth = boid.maxHealth;

		targetAcqRadius = boid.targetAcqRadius;
		targetAcqAngleMod = boid.targetAcqAngleMod;
		targetAcqMaxAngle = boid.targetAcqMaxAngle;
		targetAcqMinFitness = boid.targetAcqMinFitness;

		maxChaseDistance = boid.maxChaseDistance;
		maxFireDistance = boid.maxFireDistance;
		maxFireAngle = boid.maxFireAngle;
		accuracy = boid.accuracy;
		cooldown = boid.cooldown;
		damage = boid.damage;

		fear = boid.fear;
		maxEvadeDistance = boid.maxEvadeDistance;
	}

	public BoidData()
	{
		neighborhoodRadius = 10.0f;
		accelerationMax = 0.1f;
		velocityMax = 1.0f;
		cohesionConstant = 100;
		alignmentConstant = 20;
		separationConstant = 3;
		chaseConstant = 100;
		evadeConstant = 100;
		maxHealth = 100;

		// Target Acquisition
		targetAcqRadius = 20.0f;
		targetAcqAngleMod = 0.7f;
		targetAcqMaxAngle = 75.0f;
		targetAcqMinFitness = 0.3f;

		// Chase
		maxChaseDistance = 25.0f;
		maxFireDistance = 20.0f;
		maxFireAngle = 3.0f;
		accuracy = 0.3f;
		cooldown = 20; // In frames
		damage = 70;

		// Evade
		fear = 0.5f;
		maxEvadeDistance = 25.0f;
	}

	public void RerollPilot()
	{
		float sociability = Random.value;
		float ego = Random.value;
		float persistence = Random.value;
		float vision = Random.value;
		float skill = Random.value;

		this.cohesionConstant = CalcRandProp(70, 60, sociability, true);
		this.alignmentConstant = CalcRandProp(10, 20, ego, false);
		this.targetAcqMaxAngle = CalcRandProp(60, 40, vision, false);
		this.targetAcqMinFitness = CalcRandProp(0.2f, 0.2f, persistence, true);
		this.maxChaseDistance = CalcRandProp(targetAcqMaxAngle, 10, persistence, false);
		this.accuracy = CalcRandProp(0.3f, 0.8f, skill, false);
		this.fear = CalcRandProp(0, 1, ego, true);
	}

	public void RerollShip()
	{
		float pointsToAllocate = 6;

		float sensors = Random.value;
		float acceleration = Random.value;
		float topSpeed = Random.value;
		float armor = Random.value;
		float range = Random.value;
		float damage = Random.value;

		float sum = sensors + acceleration + topSpeed + armor + range + damage;
		sensors = sensors * pointsToAllocate / sum;
		acceleration = acceleration * pointsToAllocate / sum;
		topSpeed = topSpeed * pointsToAllocate / sum;
		armor = armor * pointsToAllocate / sum;
		range = range * pointsToAllocate / sum;
		damage = damage * pointsToAllocate / sum;

		this.neighborhoodRadius = CalcRandProp(10, 5, sensors, false);
		this.accelerationMax = CalcRandProp(0.07f, 0.1f, acceleration, false);
		this.velocityMax = CalcRandProp(0.7f, 1, topSpeed, false);
		this.maxHealth = CalcRandProp(50, 300, armor, false);
		this.targetAcqRadius = CalcRandProp(15, 10, sensors, false);
		this.maxFireDistance = CalcRandProp(15, 10, range, false);
		this.cooldown = (int)CalcRandProp(20, 70, damage, false);
		this.damage = CalcRandProp(10, 200, damage, false);
	}

	private

	float CalcRandProp(float min, float range, float rand, bool invert)
	{
		float modifier = invert ? (1 - rand) : rand;
		return range * modifier + min;
	}
}
