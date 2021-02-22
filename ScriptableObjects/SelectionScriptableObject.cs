using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Selection", menuName = "ScriptableObjects/Selection", order = 1)]
public class SelectionScriptableObject : ScriptableObject
{
    public PilotShipPair[] Value;
}
