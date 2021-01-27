using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class BattleManager : MonoBehaviour
{
	public static BattleManager Instance { get; private set; }

	public Boid[] boids;
	public Text winText;
	public GameObject winPanel;

	int allyCount;
	int enemyCount;

	PilotData pilotReward;
	ShipData shipReward;

	private void Awake()
	{
		if (Instance != null && Instance != this) {
			Destroy(this.gameObject);
		} else {
			Instance = this;
		}
	}

    // Start is called before the first frame update
    void Start()
	{
		boids = FindObjectsOfType<Boid>();
		LoadBoids();
		allyCount = 5;
		enemyCount = 5;
    }

	public void OnBoidDeath(bool isEnemy)
	{
		if (isEnemy) {
			if (--enemyCount == 0) {
				winText.text = "Victory";
				winPanel.SetActive(true);
				MasterManager.Instance.CompleteLevel(pilotReward, shipReward);
			}
		} else {
			if (--allyCount == 0) {
				winText.text = "Defeat";
				winPanel.SetActive(true);
				MasterManager.Instance.ResetGame();
			}
		}
	}

	public void ResetGame()
	{
		SceneManager.LoadScene("ShipSelectionScene", LoadSceneMode.Additive);
		SceneManager.UnloadSceneAsync("BattleScene");
	}

	public void LoadBoids()
	{
		Boid[] allies = GetAllies();
		Boid[] enemies = GetEnemies();

		for (int i = 0; i < allies.Length; i++)
		{
			PilotShipSelectionData match = MasterManager.Instance.Selection[i];
			allies[i].Initialize(
				MasterManager.Instance.Pilots[match.pilotIndex],
				MasterManager.Instance.Ships[match.shipIndex]
			);

			string pilotJson = JsonUtility.ToJson(MasterManager.Instance.Pilots[match.pilotIndex]);
			string shipJson = JsonUtility.ToJson(MasterManager.Instance.Ships[match.shipIndex]);
			Debug.Log("Pilot:\n" + pilotJson + "\nShip:\n" + shipJson);
		}

		EnemyTeamData enemyData = SaveSystem.LoadEnemyTeamJson(MasterManager.Instance.Level);
		pilotReward = enemyData.pilotReward;
		shipReward = enemyData.shipReward;
		for (int i = 0; i < allies.Length; i++)
		{
			enemies[i].Initialize(enemyData.pilots[i], (ShipScriptableObject)Resources.Load("Ships/" + enemyData.ships[i]));
		}
	}

	private

	Boid[] GetAllies()
	{
		List<Boid> filteredList  = new List<Boid>();
		foreach(Boid boid in boids) {
			if (!boid.isEnemy) {
				filteredList.Add(boid);
			}
		}
		return filteredList.ToArray();
	}

	Boid[] GetEnemies()
	{
		List<Boid> filteredList  = new List<Boid>();
		foreach(Boid boid in boids) {
			if (boid.isEnemy) {
				filteredList.Add(boid);
			}
		}
		return filteredList.ToArray();
	}
}
