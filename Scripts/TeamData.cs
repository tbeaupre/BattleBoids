using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TeamData
{
	public BoidData[] team;

	public TeamData(Boid[] boids)
	{
		team = new BoidData[5] {
			new BoidData(boids[0]),
			new BoidData(boids[1]),
			new BoidData(boids[2]),
			new BoidData(boids[3]),
			new BoidData(boids[4]),
		};
	}
}
