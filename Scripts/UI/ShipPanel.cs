using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipPanel : MonoBehaviour
{
	public ShipData ship;
	int index;

	Text buttonText;
	bool isActive = false;

	void Start()
	{
		buttonText = GetComponentInChildren<Button>().gameObject.GetComponentInChildren<Text>();

	}

	public void SetShip(ShipData newShip, int index)
	{
		ship = newShip;

		ShipAttribute[] attributes = GetComponentsInChildren<ShipAttribute>();
		attributes[0].value = ship.sensors;
		attributes[1].value = ship.acceleration;
		attributes[2].value = ship.topSpeed;
		attributes[3].value = ship.armor;
		attributes[4].value = ship.range;
		attributes[5].value = ship.damage;

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
