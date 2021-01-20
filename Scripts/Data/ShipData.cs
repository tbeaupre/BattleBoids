using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShipData
{
	public float sensors;
	public float acceleration;
	public float topSpeed;
	public float armor;
	public float range;
	public float damage;

	public ShipData()
	{
		Reroll();
	}

	public void Reroll()
	{
		float pointsToAllocate = 6;

		sensors = Random.value;
		acceleration = Random.value;
		topSpeed = Random.value;
		armor = Random.value;
		range = Random.value;
		damage = Random.value;

		float sum = sensors + acceleration + topSpeed + armor + range + damage;

		sensors = sensors * pointsToAllocate / sum;
		acceleration = acceleration * pointsToAllocate / sum;
		topSpeed = topSpeed * pointsToAllocate / sum;
		armor = armor * pointsToAllocate / sum;
		range = range * pointsToAllocate / sum;
		damage = damage * pointsToAllocate / sum;
	}
}
