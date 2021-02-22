using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotSelectionManager : MonoBehaviour
{
	public SelectionSO Selection;
	public IntVariableSO MatchSelectionIndex;
	public PilotVariableSO PilotPreview;
	public GameEventSO MatchSelectionChanged;
	public GameEventSO PilotSelectionChanged;

	public static PilotSelectionManager Instance { get; private set; }

	private void Awake()
	{
		if (Instance != null && Instance != this) {
			Destroy(this.gameObject);
		} else {
			Instance = this;
		}
	}

	public void ConfirmPilotSelection()
	{
		for (int i = 0; i < 5; i++) {
			if (Selection.Value[i].Pilot == PilotPreview.Value) {
				if (i != MatchSelectionIndex.Value) {
					Selection.Value[i].Pilot = null;
				}
			}
		}

		Selection.Value[MatchSelectionIndex.Value].Pilot = PilotPreview.Value;
		PilotSelectionChanged.Raise();

		if (++MatchSelectionIndex.Value > 4) {
			MatchSelectionIndex.Value = 0;
		}
		MatchSelectionChanged.Raise();
	}
}
