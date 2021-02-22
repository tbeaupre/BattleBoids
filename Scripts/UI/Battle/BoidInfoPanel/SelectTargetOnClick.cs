using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTargetOnClick : MonoBehaviour
{
    public SelectedBoidSO HoveredBoid;
    public SelectedBoidSO TargetBoid;

    public void OnMouseDown()
    {
        TargetBoid.Boid = TargetBoid.Boid == HoveredBoid.Boid ? null : HoveredBoid.Boid;
    }
}
