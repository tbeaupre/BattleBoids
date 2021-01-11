﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterManager : MonoBehaviour
{
	public static MasterManager Instance { get; private set; }

	public ShipData[] Ships { get; private set; }
	public PilotData[] Pilots { get; private set; }
	HashSet<int> shipSelection = new HashSet<int>();

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
			Ships = data.ships;
			Pilots = data.pilots;
//			SceneManager.LoadSceneAsync("MainMenuScene", LoadSceneMode.Additive);
		}
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
}
