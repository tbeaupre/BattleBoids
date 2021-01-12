using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipToMatchPanel : MonoBehaviour
{
	public ShipData ship;
	int index;

	public void SetShip(ShipData newShip, int index)
	{
		ship = newShip;

		ShipAttribute[] attributes = GetComponentsInChildren<ShipAttribute>();
		attributes[0].SetValue(ship.sensors);
		attributes[1].SetValue(ship.acceleration);
		attributes[2].SetValue(ship.topSpeed);
		attributes[3].SetValue(ship.armor);
		attributes[4].SetValue(ship.range);
		attributes[5].SetValue(ship.damage);

		this.index = index;
	}
}
