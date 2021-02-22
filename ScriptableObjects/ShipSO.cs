using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ship", menuName = "ScriptableObjects/Ship", order = 1)]
public class ShipSO : ScriptableObject
{
	public GameObject prefab;
    public string shipName;
	public Sprite portrait;

	public float sensors;
	public float acceleration;
	public float topSpeed;
	public float armor;
	public float range;
	public float damage;
}
