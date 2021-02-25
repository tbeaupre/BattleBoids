using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterManager : MonoBehaviour
{
	public static MasterManager Instance { get; private set; }

	public ShipSetSO Ships;
	public PilotSetSO Pilots;
	public SelectionSO Selection;
	public ShipSetSO ShipSelection;
	public BoidListSO Boids;

	public int Level { get; private set; }
	public EnemyTeamSO EnemyTeam { get; private set; }

	public List<string> startingShips = new List<string> { 
		"BlueFalcon",
		"FireStingray",
		"GoldenFox",
		"WhiteCat",
		"WildGoose"
	};

	public List<string> startingPilots = new List<string> {
		"CaptainFalcon",
		"DrStewart",
		"JodySummer",
		"Pico",
		"SamuraiGoroh"
	};

	private void Awake()
	{
		if (Instance != null && Instance != this) {
			Destroy(this.gameObject);
		} else {
			Instance = this;

			TeamData data = SaveSystem.LoadTeam();
			if (data == null) {
				Debug.Log("Creating new save file");

				data = GetFreshTeamData();
				SaveSystem.SaveTeam(data);
			}
			Level = data.level;
			PickEnemy();

			Ships.Value = new List<ShipSO>();
			foreach(string shipKey in data.ships) {
				Ships.Value.Add(Resources.Load<ShipSO>("Ships/" + shipKey));
			}
			Pilots.Value = new List<PilotSO>();
			foreach(string pilotKey in data.pilots) {
				Pilots.Value.Add(Resources.Load<PilotSO>("Pilots/" + pilotKey));
			}
			// TEST_SetReasonableDefault();
//			SceneManager.LoadSceneAsync("MainMenuScene", LoadSceneMode.Additive);
		}
	}

	void PickEnemy()
	{
		LevelSO levelSO = Resources.Load<LevelSO>("Levels/" + Level);
		int randIndex = (int)Mathf.Round(Random.value * (float)(levelSO.enemyOptions.Length - 1));
		EnemyTeam = levelSO.enemyOptions[randIndex];
	}

	TeamData GetFreshTeamData()
	{
		List<string> pilotPool = new List<string>(startingPilots);
		string[] pilots = new string[5];
		for (int i = 0; i < 5; i++) {
			int randIndex = (int)Mathf.Round(Random.value * (float)(pilotPool.Count - 1));
			pilots[i] = pilotPool[randIndex];
			pilotPool.RemoveAt(randIndex);
		}

		List<string> shipPool = new List<string>(startingShips);
		string[] ships = new string[5];
		for (int i = 0; i < 5; i++) {
			int randIndex = (int)Mathf.Round(Random.value * (float)(shipPool.Count - 1));
			ships[i] = shipPool[randIndex];
			shipPool.RemoveAt(randIndex);
		}

		return new TeamData(1, pilots, ships);
	}

	public void HandleBattleEnded()
	{
		if (Boids.EnemyCount == 0) {
			CompleteLevel();
		} else {
			ResetGame();
		}
	}

	void ResetGame()
	{
		TeamData data = GetFreshTeamData();
		SaveSystem.SaveTeam(data);
		Level = data.level;
		Ships.Value = new List<ShipSO>();
		foreach(string shipKey in data.ships) {
			Ships.Value.Add(Resources.Load<ShipSO>("Ships/" + shipKey));
		}
		Pilots.Value = new List<PilotSO>();
		foreach(string pilotKey in data.pilots) {
			Pilots.Value.Add(Resources.Load<PilotSO>("Pilots/" + pilotKey));
		}
	}

	void CompleteLevel()
	{
		Debug.Log(JsonUtility.ToJson(EnemyTeam.shipReward));
		Pilots.Value.Add(EnemyTeam.pilotReward);
		Ships.Value.Add(EnemyTeam.shipReward);
		Level++;
		PickEnemy();
		// SaveSystem.SaveTeam(new TeamData(Level, Pilots.ToArray(), Ships.ToArray()));
	}

	public void ConfirmShipSelection()
	{
		Selection.Value = new PilotShipPair[5];
		for (int i = 0; i < 5; i++) {
			Selection.Value[i] = new PilotShipPair(null, ShipSelection.Value[i]);
		}
	}
}
