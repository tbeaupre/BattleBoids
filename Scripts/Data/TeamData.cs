using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TeamData
{
	public PilotData[] pilots;
	public ShipData[] ships;

	public TeamData(PilotData[] pilotData, ShipData[] shipData)
	{
		pilots = pilotData;
		ships = shipData;
	}
}
