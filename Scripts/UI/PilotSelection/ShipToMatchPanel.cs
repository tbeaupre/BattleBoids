﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipToMatchPanel : MonoBehaviour
{
	public ShipData ship;
	int index;

	void Start()
	{
		PilotSelectionManager.Instance.SelectionChanged += HandleSelectionChanged;
		HandleSelectionChanged(0);
	}

	public void SetShip(ShipData newShip, int index)
	{
		ship = newShip;

		Attribute[] attributes = GetComponentsInChildren<Attribute>();
		attributes[0].SetValue(ship.sensors);
		attributes[1].SetValue(ship.acceleration);
		attributes[2].SetValue(ship.topSpeed);
		attributes[3].SetValue(ship.armor);
		attributes[4].SetValue(ship.range);
		attributes[5].SetValue(ship.damage);

		this.index = index;
	}

	public void HandleSelectionChanged(int index)
	{
		int newShipIndex = MasterManager.Instance.Selection[index].shipIndex;
		SetShip(MasterManager.Instance.Ships[newShipIndex], newShipIndex);
	}
}
