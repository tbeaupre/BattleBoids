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
	public float targetAcqMinAngle;
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
		targetAcqMinAngle = boid.targetAcqMinAngle;
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
}
