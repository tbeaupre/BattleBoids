using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PilotInfoPanel : MonoBehaviour
{
	public PilotData pilot;
	int index;

	Text buttonText;
	bool isActive = false;

	void Start()
	{
		buttonText = GetComponentInChildren<Button>().gameObject.GetComponentInChildren<Text>();

	}

	public void SetPilot(PilotData newPilot, int index)
	{
		pilot = newPilot;

		PilotAttribute[] attributes = GetComponentsInChildren<PilotAttribute>();
		attributes[0].SetValue(pilot.sociability);
		attributes[1].SetValue(pilot.ego);
		attributes[2].SetValue(pilot.persistence);
		attributes[3].SetValue(pilot.vision);
		attributes[4].SetValue(pilot.skill);

		this.index = index;

//		if (MasterManager.Instance.IsShipSelected(index)) {
//			isActive = true;
//			buttonText.text = "Deselect";
//		}
	}

//	public void ToggleIsActive()
//	{
//		isActive = !isActive;
//		if (isActive) {
//			buttonText.text = "Deselect";
//			MasterManager.Instance.SelectShip(index);
//		} else {
//			buttonText.text = "Select";
//			MasterManager.Instance.DeselectShip(index);
//		}
//	}
}
