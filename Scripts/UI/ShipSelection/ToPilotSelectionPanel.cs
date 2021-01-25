using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToPilotSelectionPanel : MonoBehaviour
{
	public Button toPilotSelectionButton;

	void Awake()
	{
		MasterManager.Instance.ShipSelectionChanged += ShipSelectionChangeHandler;
		toPilotSelectionButton.gameObject.SetActive(false);
	}

	void OnDisable()
	{
		MasterManager.Instance.ShipSelectionChanged -= ShipSelectionChangeHandler;
	}

	public void ShipSelectionChangeHandler(int count)
	{
		if (toPilotSelectionButton == null) {
			toPilotSelectionButton = GetComponentInChildren<Button>();
		}
		if (count == 5) {
			toPilotSelectionButton.gameObject.SetActive(true);
		} else {
			toPilotSelectionButton.gameObject.SetActive(false);
		}
	}
}
