using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowIconWhenSelected : MonoBehaviour
{
    public SelectedBoidScriptableObject TargetBoid;
    public Image IconImage;
    public BoidInfoPanel AssociatedBoidInfoPanel;

    // Update is called once per frame
    void Update()
    {
        IconImage.enabled = TargetBoid.Boid != null && AssociatedBoidInfoPanel.boid == TargetBoid.Boid;
    }
}
