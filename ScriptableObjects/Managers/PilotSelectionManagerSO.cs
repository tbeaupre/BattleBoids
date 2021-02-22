using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PilotSelectionManager", menuName = "ScriptableObjects/Managers/PilotSelectionManager")]
public class PilotSelectionManagerSO : ScriptableObject
{
    public SelectionSO Selection;
	public IntVariableSO MatchSelectionIndex;
	public PilotVariableSO PilotPreview;

	public GameEventSO MatchSelectionChanged;
	public GameEventSO PilotSelectionChanged;

    void OnEnable()
    {
        MatchSelectionIndex.Value = 0;
		PilotPreview.Value = Selection.Value[0].Pilot;
        MatchSelectionChanged.Raise();
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
		PilotPreview.Value = Selection.Value[MatchSelectionIndex.Value].Pilot;
		MatchSelectionChanged.Raise();
	}
}
