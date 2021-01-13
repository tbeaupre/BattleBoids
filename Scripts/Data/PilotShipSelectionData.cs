using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PilotShipSelectionData
{
	public int pilotIndex;
	public int shipIndex;

	public PilotShipSelectionData(int pilotIndex, int shipIndex)
	{
		this.pilotIndex = pilotIndex;
		this.shipIndex = shipIndex;
	}
}
