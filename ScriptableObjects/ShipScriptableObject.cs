using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ship", menuName = "ScriptableObjects/ShipScriptableObject", order = 1)]
public class ShipScriptableObject : ScriptableObject
{
    public string shipName;
	public Sprite portrait;

	public float sensors;
	public float acceleration;
	public float topSpeed;
	public float armor;
	public float range;
	public float damage;
}
