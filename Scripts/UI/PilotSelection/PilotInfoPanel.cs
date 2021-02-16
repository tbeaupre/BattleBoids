using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PilotInfoPanel : MonoBehaviour
{
	public PilotSet Pilots;

	public PilotScriptableObject pilot;
	public Image pilotPortrait;
	public GameObject noPilotPanel;
	public Text pilotName;
	int index;

	Text buttonText;
	Attribute[] attributes;

	void Start()
	{
		PilotSelectionManager.Instance.SelectionChanged += HandleSelectionChanged;

		buttonText = GetComponentInChildren<Button>().gameObject.GetComponentInChildren<Text>();
		attributes = GetComponentsInChildren<Attribute>();

		NoPilot();
	}

	public void SetPilot(PilotScriptableObject newPilot, int index)
	{
		pilot = newPilot;

		attributes[0].SetValue(pilot.sociability);
		attributes[1].SetValue(pilot.ego);
		attributes[2].SetValue(pilot.persistence);
		attributes[3].SetValue(pilot.vision);
		attributes[4].SetValue(pilot.skill);

		pilotPortrait.sprite = pilot.portrait;

		pilotName.text = pilot.pilotName;

		this.index = index;
		noPilotPanel.SetActive(false);
	}

	public void NoPilot()
	{
		noPilotPanel.SetActive(true);

		attributes[0].SetValue(0);
		attributes[1].SetValue(0);
		attributes[2].SetValue(0);
		attributes[3].SetValue(0);
		attributes[4].SetValue(0);

		pilotPortrait.sprite = null;

		pilotName.text = "";

		this.index = -1;
	}

	public void HandleSelectionChanged(int index)
	{
		int newPilotIndex = MasterManager.Instance.Selection[index].pilotIndex;

		if (newPilotIndex == -1) {
			NoPilot();
		} else {
			SetPilot(Pilots.Value[newPilotIndex], newPilotIndex);
		}
	}

	public void ConfirmSelection()
	{
		PilotSelectionManager.Instance.ConfirmPilotSelection(index);
	}
}
