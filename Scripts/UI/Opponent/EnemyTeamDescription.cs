using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTeamDescription : MonoBehaviour
{
    Text teamDescription;

    // Start is called before the first frame update
    void Start()
    {
        teamDescription = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        teamDescription.text = MasterManager.Instance.EnemyTeam.teamDescription;
    }
}
