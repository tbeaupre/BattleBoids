using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pilot", menuName = "ScriptableObjects/Pilot", order = 1)]
public class PilotSO : ScriptableObject
{
    public string pilotName;
    public Sprite portrait;
	public string quote;

    public float sociability;
	public float ego;
	public float persistence;
	public float vision;
	public float skill;
}
