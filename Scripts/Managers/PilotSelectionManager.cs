using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotSelectionManager : MonoBehaviour
{
	public GameObject toBattlePanel;
	public static PilotSelectionManager Instance { get; private set; }

	int selectionIndex = 0;
	int matchesCount = 0;

	// Delegates
	public delegate void SelectionChangedDelegate(int index);
	public delegate void PilotPreviewChangedDelegate(int pilotIndex);
	public delegate void PilotSelectionChangedDelegate(int selectionIndex, int fromPilotIndex, int toPilotIndex);

	// Events
	public event SelectionChangedDelegate SelectionChanged;
	public event PilotPreviewChangedDelegate PilotPreviewChanged;
	public event PilotSelectionChangedDelegate PilotSelectionChanged;

	private void Awake()
	{
		if (Instance != null && Instance != this) {
			Destroy(this.gameObject);
		} else {
			Instance = this;
		}
	}

	void Start()
	{
		toBattlePanel.SetActive(false);
	}

	public void SelectMatch(int index)
	{
		selectionIndex = index;
		SelectionChanged(index);
	}

	public void ConfirmPilotSelection(int pilotIndex)
	{
		for (int i = 0; i < 5; i++) {
			if (MasterManager.Instance.Selection[i].pilotIndex == pilotIndex) {
				matchesCount--;
				if (i != selectionIndex) {
					PilotSelectionChanged(i, MasterManager.Instance.Selection[i].pilotIndex, -1);
					MasterManager.Instance.Selection[i].pilotIndex = -1;
				}
			}
		}

		if (MasterManager.Instance.Selection[selectionIndex].pilotIndex == -1) {
			matchesCount++;
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

		toBattlePanel.SetActive(matchesCount == 5);
	}

	public void ChangePilotPreview(int pilotIndex)
	{
		PilotPreviewChanged(pilotIndex);
	}
}
