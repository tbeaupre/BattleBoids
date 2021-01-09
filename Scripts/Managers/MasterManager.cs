using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterManager : MonoBehaviour
{
	public static MasterManager Instance { get; private set; }

	private void Awake()
	{
		if (Instance != null && Instance != this) {
			Destroy(this);
		} else {
			Instance = this;
			SceneManager.LoadSceneAsync("MainMenuScene", LoadSceneMode.Additive);
		}
	}
}
