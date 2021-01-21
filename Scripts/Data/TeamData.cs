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

	public TeamData()
	{
		pilots = new PilotData[5];
		for (int i = 0; i < 5; i++) {
			pilots[i] = new PilotData();
			pilots[i].Reroll();
		}

		ships = new ShipData[5];
		for (int i = 0; i < 5; i++) {
			ships[i] = new ShipData();
			ships[i].Reroll();
		}
	}
}
