using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipPanel : MonoBehaviour
{
	public ShipSO ship;
	// public ShipData ship;
	int index;

	public Text buttonText;
	public Image image;
	bool isActive = false;

	public void SetShip(ShipSO newShip, int index)
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

		Sprite sprite = ship.portrait;
		if (sprite == null) {
			sprite = Resources.Load<Sprite>("Images/BlueFalcon");
		}
		image.sprite = sprite;
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
