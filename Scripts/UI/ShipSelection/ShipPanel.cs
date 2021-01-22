using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipPanel : MonoBehaviour
{
	public ShipData ship;
	int index;

	public Text buttonText;
	bool isActive = false;

	public void SetShip(ShipData newShip, int index)
	{
		ship = newShip;

		Attribute[] attributes = GetComponentsInChildren<Attribute>();
		attributes[0].SetValue(ship.sensors);
		attributes[1].SetValue(ship.acceleration);
		attributes[2].SetValue(ship.topSpeed);
		attributes[3].SetValue(ship.armor);
		attributes[4].SetValue(ship.range);
		attributes[5].SetValue(ship.damage);

		this.index = index;

		if (MasterManager.Instance.IsShipSelected(index)) {
			isActive = true;
			buttonText.text = "Deselect";
		}
	}

	public void ToggleIsActive()
	{
		isActive = !isActive;
		if (isActive) {
			buttonText.text = "Deselect";
			MasterManager.Instance.SelectShip(index);
		} else {
			buttonText.text = "Select";
			MasterManager.Instance.DeselectShip(index);
		}
	}
}
