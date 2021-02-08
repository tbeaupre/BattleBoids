using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCaptainPortrait : MonoBehaviour
{
    Image captainPortrait;

    // Start is called before the first frame update
    void Start()
    {
        captainPortrait = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        captainPortrait.sprite = MasterManager.Instance.EnemyTeam.pilots[0].portrait;
    }
}
