using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTeamName : MonoBehaviour
{
    Text teamName;

    // Start is called before the first frame update
    void Start()
    {
        teamName = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        teamName.text = MasterManager.Instance.EnemyTeam.teamName;
    }
}
