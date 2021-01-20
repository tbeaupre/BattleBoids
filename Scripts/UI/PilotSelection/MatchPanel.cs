using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchPanel : MonoBehaviour
{
	public int index;
	Image image;
	bool hasPilot = false;

	void Start()
	{
		PilotSelectionManager.Instance.SelectionChanged += HandleSelectionChanged;
		PilotSelectionManager.Instance.PilotSelectionChanged += HandlePilotSelectionChanged;

		image = GetComponent<Image>();

		if (index == 0) {
			image.color = Color.yellow;
		} else {
			image.color = Color.red;
		}
	}

	public void SelectMatch()
	{
		PilotSelectionManager.Instance.SelectMatch(index);
	}

	public void HandleSelectionChanged(int index)
	{
		if (index == this.index) {
			image.color = Color.yellow;
		} else {
			image.color = hasPilot ? Color.green : Color.red;
		}
	}

	public void HandlePilotSelectionChanged(int selectionIndex, int fromPilotIndex, int toPilotIndex)
	{
		if (selectionIndex == index) {
			hasPilot = toPilotIndex != -1;
		}
	}
}
