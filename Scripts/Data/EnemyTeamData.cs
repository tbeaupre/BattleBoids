using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyTeamData
{
	public PilotData[] pilots;
	public ShipData[] ships;
	public PilotData pilotReward;
	public ShipData shipReward;

	public EnemyTeamData(PilotData[] pilotData, ShipData[] shipData, PilotData pilotReward, ShipData shipReward)
	{
		pilots = pilotData;
		ships = shipData;
		this.pilotReward = pilotReward;
		this.shipReward = shipReward;
	}
}
