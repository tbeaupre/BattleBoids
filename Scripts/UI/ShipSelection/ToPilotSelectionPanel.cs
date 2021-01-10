using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToPilotSelectionPanel : MonoBehaviour
{
	Button toPilotSelectionButton;

    // Start is called before the first frame update
    void Start()
    {
		MasterManager.Instance.ShipSelectionChanged += ShipSelectionChangeHandler;

		toPilotSelectionButton = GetComponentInChildren<Button>();
		toPilotSelectionButton.gameObject.SetActive(false);
    }

	public void ShipSelectionChangeHandler(int count)
	{
		if (count == 5) {
			toPilotSelectionButton.gameObject.SetActive(true);
		} else {
			toPilotSelectionButton.gameObject.SetActive(false);
		}
	}
}
