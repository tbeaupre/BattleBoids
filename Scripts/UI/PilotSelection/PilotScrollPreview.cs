using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PilotScrollPreview : MonoBehaviour
{
	public SelectionSO Selection;
	public IntVariableSO MatchSelectionIndex;
	public PilotVariableSO PilotPreview;
	public GameEventSO PilotPreviewChanged;

	public Image pilotPortrait;

	PilotSO pilot;
	int index;
	Image image;
	Color defaultColor;

	public GameObject pilotSelectedPanel;

	void Start()
	{
		pilotSelectedPanel.SetActive(false);

		image = GetComponent<Image>();
		defaultColor = image.color;
	}

	public void SetPilot(PilotSO newPilot, int index)
	{
		pilot = newPilot;

		Text nameText = GetComponentInChildren<Text>();
		nameText.text = pilot.pilotName;

		pilotPortrait.sprite = pilot.portrait;

		this.index = index;
	}

	public void SelectPreview()
	{
		PilotPreview.Value = pilot;
		PilotPreviewChanged.Raise();
	}

	public void HandleMatchSelectionChanged()
	{
		if (Selection.Value[MatchSelectionIndex.Value].Pilot == pilot) {
			image.color = Color.yellow;
		} else {
			image.color = defaultColor;
		}
	}

	public void HandlePilotPreviewChanged()
	{
		if (PilotPreview.Value == pilot) {
			image.color = Color.yellow;
		} else {
			image.color = defaultColor;
		}
	}

	public void HandlePilotSelectionChanged()
	{
		bool isSelected = false;
		foreach (PilotShipPair match in Selection.Value) {
			if (match.Pilot == pilot) {
				isSelected = true;
			}
		}
		pilotSelectedPanel.SetActive(isSelected);
	}
}
