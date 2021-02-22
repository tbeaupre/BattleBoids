using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelectedBoid", menuName = "ScriptableObjects/SelectedBoid", order = 1)]
public class SelectedBoidSO : ScriptableObject
{
    public Boid Boid;
}
