using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightWhenHovered : MonoBehaviour
{
	public SelectedBoidSO HoveredBoid;
    public Material HoveredMaterial;
    public Boid Boid;
    
	private MeshRenderer meshRenderer;
    private bool wasHighlighted = false;

    // Start is called before the first frame update
    void Start()
    {
		meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Material[] materials = meshRenderer.materials;
    
        if (Boid != null && HoveredBoid.Boid == Boid) {
            meshRenderer.materials = new Material[2] { materials[0], HoveredMaterial };
            wasHighlighted = true;
        } else if (wasHighlighted) {
            meshRenderer.materials = new Material[1] { materials[0] };
            wasHighlighted = false;
        }
        // meshRenderer.material = HoveredBoid.Boid == Boid ? HoveredMaterial : DefaultMaterial;
    }
}
