using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoidSelectionGameManager : MonoBehaviour
{
	public void Fight()
	{
		BoidPanel[] boidPanels = FindObjectsOfType<BoidPanel>();
		BoidData[] team = new BoidData[5] {
			boidPanels[0].boidData,
			boidPanels[1].boidData,
			boidPanels[2].boidData,
			boidPanels[3].boidData,
			boidPanels[4].boidData,
		};

		foreach(BoidData boid in team) {
			string json = JsonUtility.ToJson(boid);
			Debug.Log(json);
		}

		SaveSystem.SaveTeam(new TeamData(team));
		SceneManager.LoadSceneAsync("BattleScene");
	}
}
