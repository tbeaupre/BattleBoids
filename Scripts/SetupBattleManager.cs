using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetupBattleManager : MonoBehaviour
{
    public BattleManagerSO BattleManager;

    void OnEnable()
    {
        SceneManager.sceneLoaded += HandleSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= HandleSceneLoaded;
    }

    void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "BattleScene") {
            BattleManager.Initialize();
        }
    }

}
