using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PilotScrollPreview : MonoBehaviour
{
	PilotData pilot;
	int index;

	public void SetPilot(PilotData newPilot, int index)
	{
		pilot = newPilot;

		Text nameText = GetComponentInChildren<Text>();
		nameText.text = "Tyler J. Beaupre " + index;

		this.index = index;
	}

	public void SelectPreview()
	{
		Debug.Log("I got clicked!" + index);
	}
}
