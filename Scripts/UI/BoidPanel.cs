using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoidPanel : MonoBehaviour
{
	public Text info;
	public PilotData pilotData;
	public ShipData shipData;

	public void SetData(PilotData pilot, ShipData ship)
	{
		pilotData = pilot;
		shipData = ship;
		SetInfo();
	}

	public void RerollPilot()
	{
		pilotData.Reroll();
		SetInfo();
	}

	public void RerollShip()
	{
		shipData.Reroll();
		SetInfo();
	}

	private

	void SetInfo()
	{
		info.text = "Pilot Stats:" +
			"\nsociability: " + pilotData.sociability +
			"\nego: " + pilotData.ego +
			"\npersistence: " + pilotData.persistence +
			"\nvision: " + pilotData.vision +
			"\nskill: " + pilotData.skill +
			"\n\nShip Stats:" +
			"\nsensors: " + shipData.sensors +
			"\nacceleration: " + shipData.acceleration +
			"\ntopSpeed: " + shipData.topSpeed +
			"\narmor: " + shipData.armor +
			"\nrange: " + shipData.range +
			"\ndamage: " + shipData.damage;
	}
}
