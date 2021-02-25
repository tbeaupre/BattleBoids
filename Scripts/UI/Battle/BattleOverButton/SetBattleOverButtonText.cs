using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetBattleOverButtonText : MonoBehaviour
{
    public BoidListSO Boids;

	public Text battleOverButtonText;
    
    void Update()
    {
        if (Boids.EnemyCount == 0)
        {
			battleOverButtonText.text = "Continue";
        } else if (Boids.AllyCount == 0)
        {
            battleOverButtonText.text = "Try Again";
        }
    }
}
