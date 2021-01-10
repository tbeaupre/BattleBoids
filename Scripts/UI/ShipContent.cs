using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipContent : MonoBehaviour
{
	public GameObject shipPanelPrefab;

    // Start is called before the first frame update
    void Start()
	{
		TeamData data = SaveSystem.LoadTeam();

		for (int i = 0; i < data.ships.Length; i++) {
			GameObject panelObject = Instantiate(shipPanelPrefab, transform);
			ShipPanel panel = panelObject.GetComponent<ShipPanel>();
			panel.SetShip(data.ships[i], i);
		}
    }
}
