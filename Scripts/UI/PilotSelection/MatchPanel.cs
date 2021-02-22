using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchPanel : MonoBehaviour
{
	public SelectionSO Selection;
	public ShipSetSO Ships;
	public PilotSetSO Pilots;
	public IntVariableSO MatchSelectionIndex;

	public GameEventSO MatchSelectionChanged;

	public int index;
	public Image shipPortrait;
	public Image pilotPortrait;
	Image image;
	bool hasPilot = false;

	void Start()
	{
		shipPortrait.sprite = Selection.Value[index].Ship.portrait;

		hasPilot = Selection.Value[index].Pilot != null;
		if (hasPilot) {
			pilotPortrait.sprite = Selection.Value[index].Pilot.portrait;
		}

		image = GetComponent<Image>();

		if (index == 0) {
			image.color = Color.yellow;
		} else {
			image.color = hasPilot ? Color.green : Color.red;
		}
	}

	public void SelectMatch()
	{
		MatchSelectionIndex.Value = index;
		MatchSelectionChanged.Raise();
	}

	public void HandleMatchSelectionChanged()
	{
		if (MatchSelectionIndex.Value == this.index) {
			image.color = Color.yellow;
		} else {
			image.color = hasPilot ? Color.green : Color.red;
		}
	}

	public void HandlePilotSelectionChanged()
	{
		hasPilot = Selection.Value[index].Pilot != null;
		if (hasPilot) {
			pilotPortrait.sprite = Selection.Value[index].Pilot.portrait;
		} else {
			pilotPortrait.sprite = null;
		}
	}
}
