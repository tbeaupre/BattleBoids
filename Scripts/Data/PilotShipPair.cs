using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PilotShipPair
{
    public PilotSO Pilot;
    public ShipSO Ship;

    public PilotShipPair(PilotSO pilot, ShipSO ship)
    {
        Pilot = pilot;
        Ship = ship;
    }
}
