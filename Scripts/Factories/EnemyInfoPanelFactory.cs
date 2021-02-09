using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfoPanelFactory : MonoBehaviour
{
    public static EnemyInfoPanelFactory Instance { get; private set; }
    public GameObject enemyInfoPanelPrefab;

	private void Awake()
	{
		if (Instance != null && Instance != this) {
			Destroy(this.gameObject);
		} else {
			Instance = this;
		}
	}

    public void InstantiateEnemyInfoPanel(Boid boid)
    {
        GameObject panelGO = Object.Instantiate(enemyInfoPanelPrefab, transform);
        panelGO.GetComponent<BoidInfoPanel>().boid = boid;
    }
}
