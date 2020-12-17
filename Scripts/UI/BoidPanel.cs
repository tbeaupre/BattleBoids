using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoidPanel : MonoBehaviour
{
	public Text info;
	public BoidData boidData = new BoidData();

	// Start is called before the first frame update
	void Start()
	{
		SetInfo();
	}

	public void RerollPilot()
	{
		boidData.RerollPilot();
		SetInfo();
	}

	public void RerollShip()
	{
		boidData.RerollShip();
		SetInfo();
	}

	private

	void SetInfo()
	{
		info.text = "Pilot Stats:" +
			"\ncohesionConstant: " + boidData.cohesionConstant +
			"\nalignmentConstant: " + boidData.alignmentConstant +
			"\ntargetAcqMaxAngle: " + boidData.targetAcqMaxAngle +
			"\ntargetAcqMinFitness: " + boidData.targetAcqMinFitness +
			"\nmaxChaseDistance: " + boidData.maxChaseDistance +
			"\naccuracy: " + boidData.accuracy +
			"\nfear: " + boidData.fear +
			"\n\nShip Stats:" +
			"\nneighborhoodRadius: " + boidData.neighborhoodRadius +
			"\naccelerationMax: " + boidData.accelerationMax +
			"\nvelocityMax: " + boidData.velocityMax +
			"\nmaxHealth: " + boidData.maxHealth +
			"\ntargetAcqRadius: " + boidData.targetAcqRadius +
			"\nmaxFireDistance: " + boidData.maxFireDistance +
			"\ncooldown: " + boidData.cooldown +
			"\ndamage: " + boidData.damage;
	}
}
