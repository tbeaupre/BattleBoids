using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnPointSet", menuName = "ScriptableObjects/EnemySpawnPoints", order = 1)]
public class EnemySpawnPointsSO : ScriptableObject
{
    public Vector3[] spawnPoints;
}
