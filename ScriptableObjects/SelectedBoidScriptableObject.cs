using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelectedBoid", menuName = "ScriptableObjects/SelectedBoidScriptableObject", order = 1)]
public class SelectedBoidScriptableObject : ScriptableObject
{
    public Boid Boid;
}
