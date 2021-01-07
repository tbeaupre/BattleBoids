using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
	public void Play()
	{
		SceneManager.LoadSceneAsync("ShipSelectionScene", LoadSceneMode.Additive);
		SceneManager.UnloadSceneAsync("MainMenuScene");
	}
}
