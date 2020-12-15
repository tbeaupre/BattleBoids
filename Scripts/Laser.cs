using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
	int life = 3;

    // Update is called once per frame
    void Update()
    {
		life--;
		if (life < 0) {
			GameObject.Destroy(gameObject);
		}
    }
}
