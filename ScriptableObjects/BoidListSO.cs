using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "BoidList", menuName = "ScriptableObjects/BoidList")]
public class BoidListSO : ScriptableObject
{
    public GameEventSO BattleEnded;
    public GameEventSO BoidKilled;

    public List<Boid> Value;
    public int AllyCount;
    public int EnemyCount;

    public void ResetBoidList()
    {
        Value = new List<Boid>();
        AllyCount = 0;
        EnemyCount = 0;
    }

    public void RegisterBoid(Boid boid)
    {
        Value.Add(boid);
        if (boid.isEnemy) {
            EnemyCount++;
        } else {
            AllyCount++;
        }
    }

    public void UnregisterBoid(Boid boid)
    {
        Value.Remove(boid);
        if (boid.isEnemy) {
            if (--EnemyCount == 0) {
                BattleEnded.Raise();
            }
        } else {
            if (--AllyCount == 0) {
                BattleEnded.Raise();
            }
        }
        BoidKilled.Raise();
    }
}
