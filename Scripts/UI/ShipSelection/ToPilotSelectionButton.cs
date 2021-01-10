using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToPilotSelectionButton : MonoBehaviour
{
	public void ToPilotSelection()
	{
		SceneManager.LoadSceneAsync("PilotSelectionScene", LoadSceneMode.Additive);
		SceneManager.UnloadSceneAsync("ShipSelectionScene");
	}
}
