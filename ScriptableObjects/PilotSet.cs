using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PilotSet", menuName = "ScriptableObjects/PilotSet", order = 1)]
public class PilotSet : ScriptableObject
{
    public List<PilotScriptableObject> Value;
}
