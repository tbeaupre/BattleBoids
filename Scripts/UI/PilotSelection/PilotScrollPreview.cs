using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PilotScrollPreview : MonoBehaviour
{
	public Image pilotPortrait;

	PilotScriptableObject pilot;
	int index;
	Image image;
	Color defaultColor;

	public PilotInfoPanel pilotInfoPanel;
	public GameObject pilotSelectedPanel;

	void Start()
	{
		PilotSelectionManager.Instance.SelectionChanged += HandleSelectionChanged;
		PilotSelectionManager.Instance.PilotPreviewChanged += HandlePilotPreviewChanged;
		PilotSelectionManager.Instance.PilotSelectionChanged += HandlePilotSelectionChanged;

		pilotInfoPanel = GameObject.Find("PilotInfoPanel").GetComponent<PilotInfoPanel>();
		pilotSelectedPanel.SetActive(false);

		image = GetComponent<Image>();
		defaultColor = image.color;
	}

	public void SetPilot(PilotScriptableObject newPilot, int index)
	{
		pilot = newPilot;

		Text nameText = GetComponentInChildren<Text>();
		nameText.text = pilot.pilotName;

		pilotPortrait.sprite = pilot.portrait;

		this.index = index;
	}

	public void SelectPreview()
	{
		pilotInfoPanel.SetPilot(pilot, index);
		PilotSelectionManager.Instance.ChangePilotPreview(index);
	}

	public void HandleSelectionChanged(int selectionIndex)
	{
		if (MasterManager.Instance.Selection[selectionIndex].pilotIndex == index) {
			image.color = Color.yellow;
		} else {
			image.color = defaultColor;
		}
	}

	public void HandlePilotPreviewChanged(int pilotIndex)
	{
		if (pilotIndex == index) {
			image.color = Color.yellow;
		} else {
			image.color = defaultColor;
		}
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
