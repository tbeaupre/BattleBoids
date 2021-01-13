using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToPilotSelectionButton : MonoBehaviour
{
	public void ToPilotSelection()
	{
		MasterManager.Instance.ConfirmShipSelection();
		SceneManager.LoadSceneAsync("PilotSelectionScene", LoadSceneMode.Additive);
		SceneManager.UnloadSceneAsync("ShipSelectionScene");
	}
}
