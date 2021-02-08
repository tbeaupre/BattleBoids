using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/EnemyTeamScriptableObject", order = 1)]
public class EnemyTeamScriptableObject : ScriptableObject
{
    public string teamName;
    public string teamDescription;

    public PilotScriptableObject[] pilots;
    public ShipScriptableObject[] ships;

    public PilotScriptableObject pilotReward;
    public ShipScriptableObject shipReward;

    public EnemySpawnPointsScriptableObject spawnPoints;
}
