using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipContent : MonoBehaviour
{
	public GameObject shipPanelPrefab;

    // Start is called before the first frame update
    void Start()
	{
		for (int i = 0; i < MasterManager.Instance.Ships.Count; i++) {
			GameObject panelObject = Instantiate(shipPanelPrefab, transform);
			ShipPanel panel = panelObject.GetComponent<ShipPanel>();
			panel.SetShip(MasterManager.Instance.Ships[i], i);
		}
    }
}
