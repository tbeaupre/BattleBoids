using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PilotScrollPreview : MonoBehaviour
{
	PilotData pilot;
	int index;

	public PilotInfoPanel pilotInfoPanel;
	public GameObject pilotSelectedPanel;

	void Start()
	{
		PilotSelectionManager.Instance.PilotSelectionChanged += HandlePilotSelectionChanged;
		pilotInfoPanel = GameObject.Find("PilotInfoPanel").GetComponent<PilotInfoPanel>();
		pilotSelectedPanel.SetActive(false);
	}

	public void SetPilot(PilotData newPilot, int index)
	{
		pilot = newPilot;

		Text nameText = GetComponentInChildren<Text>();
		nameText.text = "Tyler J. Beaupre " + index;

		this.index = index;
	}

	public void SelectPreview()
	{
		pilotInfoPanel.SetPilot(pilot, index);
	}

	public void HandlePilotSelectionChanged(int selectionIndex, int fromPilotIndex, int toPilotIndex)
	{
		if (fromPilotIndex == index) {
			pilotSelectedPanel.SetActive(false);
		}
		if (toPilotIndex == index) {
			pilotSelectedPanel.SetActive(true);
		}
	}
}
