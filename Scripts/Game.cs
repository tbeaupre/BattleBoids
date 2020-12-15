using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
	public Boid[] boids;
	public Text winText;

    // Start is called before the first frame update
    void Start()
	{
		boids = FindObjectsOfType<Boid>();
    }

    // Update is called once per frame
    void Update()
    {
		int allyCount = 0;
		int enemyCount = 0;
		foreach(Boid boid in boids) {
			if (boid != null) {
				if (boid.isEnemy) {
					enemyCount++;
				} else {
					allyCount++;
				}
			}
		}

		if (enemyCount == 0) {
			winText.text = "Victory";
			winText.gameObject.SetActive(true);
			Destroy(this);
		} else if (allyCount == 0) {
			winText.text = "Defeat";
			winText.gameObject.SetActive(true);
			Destroy(this);
		}
    }
}
