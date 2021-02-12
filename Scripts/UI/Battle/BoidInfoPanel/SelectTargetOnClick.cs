using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTargetOnClick : MonoBehaviour
{
    public SelectedBoidScriptableObject HoveredBoid;
    public SelectedBoidScriptableObject TargetBoid;

    public void OnMouseDown()
    {
        TargetBoid.Boid = TargetBoid.Boid == HoveredBoid.Boid ? null : HoveredBoid.Boid;
    }
}
