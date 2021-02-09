using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyFactory
{
    public static void InstantiateTeam(EnemyTeamScriptableObject enemyTeam) 
    {
        for (int i = 0; i < enemyTeam.ships.Length; i++) {
            GameObject enemy = Object.Instantiate(
                enemyTeam.ships[i].prefab,
                enemyTeam.spawnPoints.spawnPoints[i],
                Quaternion.identity
            );
            enemy.GetComponent<MeshRenderer>().material = Resources.Load<Material>("EnemyMaterial");
            Boid boid = enemy.GetComponent<Boid>();
            boid.isEnemy = true;
            boid.Initialize(enemyTeam.pilots[i], enemyTeam.ships[i]);

            EnemyInfoPanelFactory.Instance.InstantiateEnemyInfoPanel(boid);
        }
    }
}
