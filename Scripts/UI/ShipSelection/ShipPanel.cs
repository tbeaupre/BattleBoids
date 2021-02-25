using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipPanel : MonoBehaviour
{
	public GameEventSO ShipSelectionChanged;

	public ShipSetSO ShipSelection;

	public Text buttonText;
	public Image shipPortraitImage;
	public Image panelImage;
	public Color defaultColor;

	private ShipSO ship;
	private bool isActive = false;

	public void SetShip(ShipSO newShip)
	{
		ship = newShip;

		Attribute[] attributes = GetComponentsInChildren<Attribute>();
		attributes[0].SetValue(ship.sensors);
		attributes[1].SetValue(ship.acceleration);
		attributes[2].SetValue(ship.topSpeed);
		attributes[3].SetValue(ship.armor);
		attributes[4].SetValue(ship.range);
		attributes[5].SetValue(ship.damage);

		// Check if the ship is selected or not.
		isActive = ShipSelection.Value.Contains(ship);
		if (isActive) {
			panelImage.color = Color.yellow;
			buttonText.text = "Deselect";
		} else {
			buttonText.text = "Select";
		}

		Sprite sprite = ship.portrait;
		if (sprite == null) {
			sprite = Resources.Load<Sprite>("Images/BlueFalcon");
		}
		shipPortraitImage.sprite = sprite;
	}

	public void ToggleIsActive()
	{
		isActive = !isActive;
		if (isActive) {
			panelImage.color = Color.yellow;
			buttonText.text = "Deselect";
			ShipSelection.Value.Add(ship);
		} else {
			panelImage.color = defaultColor;
			buttonText.text = "Select";
			ShipSelection.Value.Remove(ship);
		}
		ShipSelectionChanged.Raise();
	}
}
