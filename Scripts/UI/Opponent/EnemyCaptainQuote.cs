using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCaptainQuote : MonoBehaviour
{
    Text captainQuote;

    // Start is called before the first frame update
    void Start()
    {
        captainQuote = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        captainQuote.text = MasterManager.Instance.EnemyTeam.pilots[0].quote;
    }
}
