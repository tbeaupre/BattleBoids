using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterManager : MonoBehaviour
{
	public static MasterManager Instance { get; private set; }

	public int Level { get; private set; }
	public List<ShipScriptableObject> Ships { get; private set; }
	public List<PilotScriptableObject> Pilots { get; private set; }
	List<int> shipSelection = new List<int>();
	public PilotShipSelectionData[] Selection { get; private set; }

	// Events
	public delegate void ShipSelectionChangedDelegate(int count);
	public event ShipSelectionChangedDelegate ShipSelectionChanged;

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
			Ships = new List<ShipScriptableObject>();
			foreach(string shipKey in data.ships) {
				Ships.Add(Resources.Load<ShipScriptableObject>("Ships/" + shipKey));
			}
			Pilots = new List<PilotScriptableObject>();
			foreach(string pilotKey in data.pilots) {
				Pilots.Add(Resources.Load<PilotScriptableObject>("Pilots/" + pilotKey));
			}
			// TEST_SetReasonableDefault();
//			SceneManager.LoadSceneAsync("MainMenuScene", LoadSceneMode.Additive);
		}
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

	void TEST_SetReasonableDefault()
	{
		Selection = new PilotShipSelectionData[5];
		for (int i = 0; i < 5; i++) {
			Selection[i] = new PilotShipSelectionData(i, i);
		}
	}

	public void ResetGame()
	{
		TeamData data = GetFreshTeamData();
		SaveSystem.SaveTeam(data);
		Level = data.level;
		Ships = new List<ShipScriptableObject>();
		foreach(string shipKey in data.ships) {
			Ships.Add(Resources.Load<ShipScriptableObject>("Ships/" + shipKey));
		}
		Pilots = new List<PilotScriptableObject>();
		foreach(string pilotKey in data.pilots) {
			Pilots.Add(Resources.Load<PilotScriptableObject>("Pilots/" + pilotKey));
		}
	}

	public void CompleteLevel(PilotScriptableObject pilotReward, ShipScriptableObject shipReward)
	{
		Debug.Log(JsonUtility.ToJson(shipReward));
		Level++;
		Pilots.Add(pilotReward);
		Ships.Add(shipReward);
		// SaveSystem.SaveTeam(new TeamData(Level, Pilots.ToArray(), Ships.ToArray()));
	}

	public void SelectShip(int index)
	{
		shipSelection.Add(index);
		ShipSelectionChanged(shipSelection.Count);
	}

	public void DeselectShip(int index)
	{
		shipSelection.Remove(index);
		ShipSelectionChanged(shipSelection.Count);
	}

	public bool IsShipSelected(int index)
	{
		return shipSelection.Contains(index);
	}

	public void ConfirmShipSelection()
	{
		Selection = new PilotShipSelectionData[5];
		for (int i = 0; i < 5; i++) {
			Selection[i] = new PilotShipSelectionData(-1, shipSelection[i]);
		}
	}
}
