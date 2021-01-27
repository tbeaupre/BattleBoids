using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyTeamData
{
	public string[] pilots;
	public string[] ships;
	public PilotData pilotReward;
	public ShipData shipReward;

	public EnemyTeamData(string[] pilotData, string[] shipData, PilotData pilotReward, ShipData shipReward)
	{
		pilots = pilotData;
		ships = shipData;
		this.pilotReward = pilotReward;
		this.shipReward = shipReward;
	}
}
