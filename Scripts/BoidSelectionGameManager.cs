using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class BoidSelectionGameManager : MonoBehaviour
{
	public void Fight()
	{
		BoidPanel[] boidPanels = FindObjectsOfType<BoidPanel>();
		PilotData[] pilots = boidPanels.Select(boidPanel => boidPanel.pilotData).ToArray();
		ShipData[] ships = boidPanels.Select(boidPanel => boidPanel.shipData).ToArray();

		SaveSystem.SaveTeam(new TeamData(pilots, ships));
		SceneManager.LoadSceneAsync("BattleScene");
	}
}
