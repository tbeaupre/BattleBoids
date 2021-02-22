using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetShipSelection : MonoBehaviour
{
	public GameEventSO ShipSelectionChanged;

    void Start()
    {
        ShipSelectionChanged.Raise();
    }
}
