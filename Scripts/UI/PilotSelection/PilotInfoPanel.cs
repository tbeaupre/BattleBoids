using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PilotInfoPanel : MonoBehaviour
{
	public SelectionSO Selection;
	public PilotSetSO Pilots;
	public IntVariableSO MatchSelectionIndex;
	public PilotVariableSO PilotPreview;

	public PilotSO pilot;
	public Image pilotPortrait;
	public GameObject noPilotPanel;
	public Text pilotName;

	Text buttonText;
	Attribute[] attributes;

	void Start()
	{
		buttonText = GetComponentInChildren<Button>().gameObject.GetComponentInChildren<Text>();
		attributes = GetComponentsInChildren<Attribute>();

		NoPilot();
	}

	public void SetPilot(PilotSO newPilot)
	{
		pilot = newPilot;

		if (pilot == null) {
			NoPilot();
		} else {
			attributes[0].SetValue(pilot.sociability);
			attributes[1].SetValue(pilot.ego);
			attributes[2].SetValue(pilot.persistence);
			attributes[3].SetValue(pilot.vision);
			attributes[4].SetValue(pilot.skill);

			pilotPortrait.sprite = pilot.portrait;

			pilotName.text = pilot.pilotName;

			noPilotPanel.SetActive(false);
		}
	}

	public void NoPilot()
	{
		noPilotPanel.SetActive(true);

		pilot = null;

		attributes[0].SetValue(0);
		attributes[1].SetValue(0);
		attributes[2].SetValue(0);
		attributes[3].SetValue(0);
		attributes[4].SetValue(0);

		pilotPortrait.sprite = null;

		pilotName.text = "";
	}

	public void HandleMatchSelectionChanged()
	{
		PilotSO newPilot = Selection.Value[MatchSelectionIndex.Value].Pilot;
		SetPilot(newPilot);
	}

	public void HandlePilotPreviewChanged()
	{
		SetPilot(PilotPreview.Value);
	}

	public void ConfirmSelection()
	{
		PilotSelectionManager.Instance.ConfirmPilotSelection();
	}
}
