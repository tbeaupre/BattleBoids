using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideBattleOverPanel : MonoBehaviour
{
    public BoidListSO Boids;

	public GameObject battleOverPanel;

    void Start()
    {
        battleOverPanel.SetActive(false);
    }
    
    public void HandleBattleEnded()
    {
        battleOverPanel.SetActive(true);
    }
}
