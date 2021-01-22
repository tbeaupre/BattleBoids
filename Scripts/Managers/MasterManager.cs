using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterManager : MonoBehaviour
{
	public static MasterManager Instance { get; private set; }

	public int Level { get; private set; }
	public List<ShipData> Ships { get; private set; }
	public List<PilotData> Pilots { get; private set; }
	List<int> shipSelection = new List<int>();
	public PilotShipSelectionData[] Selection { get; private set; }

	// Events
	public delegate void ShipSelectionChangedDelegate(int count);
	public event ShipSelectionChangedDelegate ShipSelectionChanged;

	private void Awake()
	{
		if (Instance != null && Instance != this) {
			Destroy(this.gameObject);
		} else {
			Instance = this;

			TeamData data = SaveSystem.LoadTeam();
			if (data == null) {
				Debug.Log("Creating new save file");
				data = new TeamData();
				SaveSystem.SaveTeam(data);
			}
			Level = data.level;
			Ships = new List<ShipData>(data.ships);
			Pilots = new List<PilotData>(data.pilots);
//			SceneManager.LoadSceneAsync("MainMenuScene", LoadSceneMode.Additive);
		}
	}

	public void ResetGame()
	{
		TeamData data = new TeamData();
		SaveSystem.SaveTeam(data);
		Level = data.level;
		Ships = new List<ShipData>(data.ships);
		Pilots = new List<PilotData>(data.pilots);
	}

	public void CompleteLevel(PilotData pilotReward, ShipData shipReward)
	{
		Level++;
		// Pilots += pilotReward;
		// Ships += shipReward;
		SaveSystem.SaveTeam(new TeamData(Level, Pilots.ToArray(), Ships.ToArray()));
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
