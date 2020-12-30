using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPanel : MonoBehaviour
{
	public ShipData ship;

	public void SetShip(ShipData newShip)
	{
		ship = newShip;

		ShipAttribute[] attributes = GetComponentsInChildren<ShipAttribute>();
		attributes[0].value = ship.sensors;
		attributes[1].value = ship.acceleration;
		attributes[2].value = ship.topSpeed;
		attributes[3].value = ship.armor;
		attributes[4].value = ship.range;
		attributes[5].value = ship.damage;
	}
}
