using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCaptainName : MonoBehaviour
{
    Text captainName;

    // Start is called before the first frame update
    void Start()
    {
        captainName = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        captainName.text = MasterManager.Instance.EnemyTeam.pilots[0].pilotName;
    }
}
