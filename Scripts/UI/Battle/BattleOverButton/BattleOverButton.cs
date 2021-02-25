using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleOverButton : MonoBehaviour
{
    public BoidListSO Boids;

    public void ResetGame()
	{
        if (Boids.AllyCount == 0) {
            SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Additive);
        } else {
            SceneManager.LoadScene("OpponentScene", LoadSceneMode.Additive);
        }
		SceneManager.UnloadSceneAsync("BattleScene");
	}
}
