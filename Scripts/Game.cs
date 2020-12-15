using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
	public Boid[] boids;
	public Text winText;
	public GameObject winPanel; 

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
			winPanel.SetActive(true);
		} else if (allyCount == 0) {
			winText.text = "Defeat";
			winPanel.SetActive(true);
		}
    }

	public void ResetGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
