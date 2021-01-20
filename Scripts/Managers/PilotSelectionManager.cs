using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotSelectionManager : MonoBehaviour
{
	public static PilotSelectionManager Instance { get; private set; }

	int selectionIndex = 0;

	// Delegates
	public delegate void SelectionChangedDelegate(int index);
	public delegate void PilotSelectionChangedDelegate(int selectionIndex, int fromPilotIndex, int toPilotIndex);

	// Events
	public event SelectionChangedDelegate SelectionChanged;
	public event PilotSelectionChangedDelegate PilotSelectionChanged;

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
		for (int i = 0; i < 5; i++) {
			if (MasterManager.Instance.Selection[i].pilotIndex == pilotIndex && i != selectionIndex) {
				PilotSelectionChanged(i, MasterManager.Instance.Selection[i].pilotIndex, -1);
				MasterManager.Instance.Selection[i].pilotIndex = -1;
			}
		}

		PilotSelectionChanged(
			selectionIndex,
			MasterManager.Instance.Selection[selectionIndex].pilotIndex,
			pilotIndex
		);
		MasterManager.Instance.Selection[selectionIndex].pilotIndex = pilotIndex;

		if (++selectionIndex > 4) {
			selectionIndex = 0;
		}
		SelectionChanged(selectionIndex);
	}
}
