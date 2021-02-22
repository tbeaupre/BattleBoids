using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PilotVar", menuName = "ScriptableObjects/PilotVariable", order = 1)]
public class PilotVariableSO : ScriptableObject
{
    public PilotScriptableObject Value;
}
