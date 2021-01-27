using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class BoidSelectionGameManager : MonoBehaviour
{
	BoidPanel[] boidPanels;

	// Start is called before the first frame update
	void Start()
	{
		boidPanels = FindObjectsOfType<BoidPanel>();
		TeamData team = SaveSystem.LoadTeam();

		for (int i = 0; i < boidPanels.Length; i++) {
			// boidPanels[i].SetData(team.pilots[i], team.ships[i]);
		}
	}

	public void Fight()
	{
		PilotData[] pilots = boidPanels.Select(boidPanel => boidPanel.pilotData).ToArray();
		ShipData[] ships = boidPanels.Select(boidPanel => boidPanel.shipData).ToArray();

		// SaveSystem.SaveTeam(new TeamData(1, pilots, ships));
		SceneManager.LoadSceneAsync("BattleScene");
	}
}
