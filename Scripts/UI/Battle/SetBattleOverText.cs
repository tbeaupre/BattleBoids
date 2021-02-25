using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetBattleOverText : MonoBehaviour
{
    public BoidListSO Boids;

	public Text winText;
    
    void Update()
    {
        if (Boids.EnemyCount == 0)
        {
			winText.text = "Victory";
        } else if (Boids.AllyCount == 0)
        {
            winText.text = "Defeat";
        }
    }
}
