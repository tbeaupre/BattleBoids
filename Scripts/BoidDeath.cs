using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidDeath : MonoBehaviour
{
    private const int MAX_DEATH_FRAMES = 120;

    public GameObject ExplosionPrefab;
    public Vector3 Velocity;

    private int deathFrameTimer;

  // Start is called before the first frame update
    void Start()
    {
        deathFrameTimer = Random.Range(0, MAX_DEATH_FRAMES);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Velocity;
        if (--deathFrameTimer < 0) {
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
