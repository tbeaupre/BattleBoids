using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TeamData
{
	public int level;
	public PilotData[] pilots;
	public ShipData[] ships;

	public TeamData(int level, PilotData[] pilotData, ShipData[] shipData)
	{
		this.level = level;
		pilots = pilotData;
		ships = shipData;
	}

	public TeamData()
	{
		level = 1;
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
