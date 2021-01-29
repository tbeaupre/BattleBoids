using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnPointSet", menuName = "ScriptableObjects/EnemySpawnPointsScriptableObject", order = 1)]
public class EnemySpawnPointsScriptableObject : ScriptableObject
{
    public Vector3[] spawnPoints;
}
