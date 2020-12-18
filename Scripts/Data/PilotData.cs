using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PilotData
{
	public float sociability;
	public float ego;
	public float persistence;
	public float vision;
	public float skill;

	public void Reroll()
	{
		sociability = Random.value;
		ego = Random.value;
		persistence = Random.value;
		vision = Random.value;
		skill = Random.value;
	}
}
