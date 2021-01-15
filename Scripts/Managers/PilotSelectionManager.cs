using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotSelectionManager : MonoBehaviour
{
	public static PilotSelectionManager Instance { get; private set; }

	int selectionIndex = 0;

	// Events
	public delegate void SelectionChangedDelegate(int index);
	public event SelectionChangedDelegate SelectionChanged;

	private void Awake()
	{
		if (Instance != null && Instance != this) {
			Destroy(this.gameObject);
		} else {
			Instance = this;
		}
	}

	public void SelectMatch(int index)
	{
		selectionIndex = index;
		SelectionChanged(index);
	}

	public void ConfirmPilotSelection(int pilotIndex)
	{
		MasterManager.Instance.Selection[selectionIndex].pilotIndex = pilotIndex;
		if (++selectionIndex > 4) {
			selectionIndex = 0;
		}
		SelectionChanged(selectionIndex);
	}
}
