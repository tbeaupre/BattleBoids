using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToPilotSelectionPanel : MonoBehaviour
{
	public ShipSetSO ShipSelection;

	private Button toPilotSelectionButton;

	void Awake()
	{
		toPilotSelectionButton = GetComponentInChildren<Button>();
		toPilotSelectionButton.gameObject.SetActive(ShipSelection.Value.Count == 5);
	}

	public void HandleShipSelectionChanged()
	{
		toPilotSelectionButton.gameObject.SetActive(ShipSelection.Value.Count == 5);
	}
}
