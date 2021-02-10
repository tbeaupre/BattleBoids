using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoidInfoPanel : MonoBehaviour
{
    public Boid boid;
    public SelectedBoidScriptableObject HoveredBoid;

    private Portrait pilotPortrait;
    private HealthBar boidHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        pilotPortrait = GetComponentInChildren<Portrait>();
        boidHealthBar = GetComponentInChildren<HealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boid != null) {
            if (boid.pilot != null) {
                pilotPortrait.portraitSprite = boid.pilot.portrait;
            }
            boidHealthBar.maxHealth = boid.maxHealth;
            boidHealthBar.currentHealth = boid.currentHealth;
        } else {
            boidHealthBar.currentHealth = 0;
        }
    }

    public void OnMouseEnter()
    {
        HoveredBoid.Boid = boid;
    }

    public void OnMouseExit()
    {
        HoveredBoid.Boid = null;
    }
}
