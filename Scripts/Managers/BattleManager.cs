﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class BattleManager : MonoBehaviour
{
	public Boid[] boids;
	public Text winText;
	public GameObject winPanel; 

    // Start is called before the first frame update
    void Start()
	{
		boids = FindObjectsOfType<Boid>();
		LoadBoids();
    }

    // Update is called once per frame
    void Update()
    {
		int allyCount = 0;
		int enemyCount = 0;
		foreach(Boid boid in boids) {
			if (boid != null) {
				if (boid.isEnemy) {
					enemyCount++;
				} else {
					allyCount++;
				}
			}
		}

		if (enemyCount == 0) {
			winText.text = "Victory";
			winPanel.SetActive(true);
		} else if (allyCount == 0) {
			winText.text = "Defeat";
			winPanel.SetActive(true);
		}
    }

	public void ResetGame()
	{
		SceneManager.LoadScene("ShipSelectionScene", LoadSceneMode.Additive);
		SceneManager.UnloadSceneAsync("BattleScene");
	}

	public void LoadBoids()
	{
		Boid[] allies = GetAllies();
		Boid[] enemies = GetEnemies();

		for (int i = 0; i < allies.Length; i++)
		{
			PilotShipSelectionData match = MasterManager.Instance.Selection[i];
			allies[i].Initialize(
				MasterManager.Instance.Pilots[match.pilotIndex],
				MasterManager.Instance.Ships[match.shipIndex]
			);

			string pilotJson = JsonUtility.ToJson(MasterManager.Instance.Pilots[match.pilotIndex]);
			string shipJson = JsonUtility.ToJson(MasterManager.Instance.Ships[match.shipIndex]);
			Debug.Log("Pilot:\n" + pilotJson + "\nShip:\n" + shipJson);
		}

		TeamData enemyData = SaveSystem.LoadTeamJson(0);
		for (int i = 0; i < allies.Length; i++)
		{
			enemies[i].Initialize(enemyData.pilots[i], enemyData.ships[i]);
		}
	}

	private

	Boid[] GetAllies()
	{
		List<Boid> filteredList  = new List<Boid>();
		foreach(Boid boid in boids) {
			if (!boid.isEnemy) {
				filteredList.Add(boid);
			}
		}
		return filteredList.ToArray();
	}

	Boid[] GetEnemies()
	{
		List<Boid> filteredList  = new List<Boid>();
		foreach(Boid boid in boids) {
			if (boid.isEnemy) {
				filteredList.Add(boid);
			}
		}
		return filteredList.ToArray();
	}
}
