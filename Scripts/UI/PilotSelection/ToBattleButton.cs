using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToBattleButton : MonoBehaviour
{
    public void ToBattle()
	{
		SceneManager.LoadSceneAsync("BattleScene", LoadSceneMode.Additive);
		SceneManager.UnloadSceneAsync("PilotSelectionScene");
	}
}
