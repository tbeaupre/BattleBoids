using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipSet", menuName = "ScriptableObjects/ShipSet", order = 1)]
public class ShipSet : ScriptableObject
{
    public List<ShipScriptableObject> Value;
}
