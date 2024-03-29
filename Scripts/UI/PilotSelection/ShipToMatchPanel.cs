﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipToMatchPanel : MonoBehaviour
{
	public ShipSetSO Ships;
	public IntVariableSO MatchSelectionIndex;
	public SelectionSO Selection;

	public ShipSO ship;
	public Image shipPortrait;

	void Start()
	{
		HandleMatchSelectionChanged();
	}

	public void SetShip(ShipSO newShip)
	{
		ship = newShip;

		Attribute[] attributes = GetComponentsInChildren<Attribute>();
		attributes[0].SetValue(ship.sensors);
		attributes[1].SetValue(ship.acceleration);
		attributes[2].SetValue(ship.topSpeed);
		attributes[3].SetValue(ship.armor);
		attributes[4].SetValue(ship.range);
		attributes[5].SetValue(ship.damage);

		shipPortrait.sprite = ship.portrait;
	}

	public void HandleMatchSelectionChanged()
	{
		SetShip(Selection.Value[MatchSelectionIndex.Value].Ship);
	}
}
