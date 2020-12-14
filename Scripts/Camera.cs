using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
	public Boid[] boids;

    // Start is called before the first frame update
    void Start()
	{
		boids = FindObjectsOfType<Boid>();
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 perceivedCenter = Vector3.zero;
		int count = 0;
		foreach(Boid boid in boids) {
			if (boid != null) {
				count++;
				perceivedCenter += boid.gameObject.transform.position;
			}
		}

		if (count != 0) {
			perceivedCenter = perceivedCenter / count;
			Vector3 displacement = perceivedCenter - transform.position;
			transform.forward = displacement;
		}
    }
}
