using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipContent : MonoBehaviour
{
	public ShipSetSO Ships;
	public GameObject shipPanelPrefab;

    // Start is called before the first frame update
    void Start()
	{
		foreach (ShipSO ship in Ships.Value) {
			GameObject panelObject = Instantiate(shipPanelPrefab, transform);
			ShipPanel panel = panelObject.GetComponent<ShipPanel>();
			panel.SetShip(ship);
		}
    }
}
