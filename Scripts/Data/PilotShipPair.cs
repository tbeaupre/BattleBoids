using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PilotShipPair
{
    public PilotScriptableObject Pilot;
    public ShipScriptableObject Ship;

    public PilotShipPair(PilotScriptableObject pilot, ShipScriptableObject ship)
    {
        Pilot = pilot;
        Ship = ship;
    }
}
