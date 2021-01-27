using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pilot", menuName = "ScriptableObjects/PilotScriptableObject", order = 1)]
public class PilotScriptableObject : ScriptableObject
{
    public string pilotName;
    public Sprite portrait;

    public float sociability;
	public float ego;
	public float persistence;
	public float vision;
	public float skill;
}
