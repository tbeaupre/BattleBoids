using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "BattleManager", menuName = "ScriptableObjects/Managers/BattleManager")]
public class BattleManagerSO : ScriptableObject
{
    public BoidListSO Boids;
	public SelectionSO Selection;

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
            Initialize();
        }
    }

    private void Initialize()
    {
        Boids.ResetBoidList();
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("BattleScene"));
		LoadBoids();
    }

    private void LoadBoids()
	{
		Boid[] allies = FindObjectsOfType<Boid>();

		for (int i = 0; i < allies.Length; i++)
		{
			PilotShipPair match = Selection.Value[i];
			allies[i].Initialize(match.Pilot, match.Ship);

			string pilotJson = JsonUtility.ToJson(match.Pilot);
			string shipJson = JsonUtility.ToJson(match.Ship);
			Debug.Log("Pilot:\n" + pilotJson + "\nShip:\n" + shipJson);
		}

		EnemyFactory.InstantiateTeam(MasterManager.Instance.EnemyTeam);
	}
}
