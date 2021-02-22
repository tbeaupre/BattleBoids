using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/EnemyTeam", order = 1)]
public class EnemyTeamSO : ScriptableObject
{
    public string teamName;
    public string teamDescription;

    public PilotSO[] pilots;
    public ShipSO[] ships;

    public PilotSO pilotReward;
    public ShipSO shipReward;

    public EnemySpawnPointsSO spawnPoints;
}
