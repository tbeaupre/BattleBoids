using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideToBattlePanel : MonoBehaviour
{
    public SelectionScriptableObject Selection;
    public GameObject ToBattlePanel;

    // Start is called before the first frame update
    void Start()
    {
        HandlePilotSelectionChanged();
    }

    public void HandlePilotSelectionChanged()
    {
        ToBattlePanel.SetActive(AreAllPilotsSelected());
    }

    private bool AreAllPilotsSelected()
    {
        foreach(PilotShipPair match in Selection.Value)
        {
            if (match.Pilot == null)
            {
                return false;
            }
        }
        return true;
    }
}
