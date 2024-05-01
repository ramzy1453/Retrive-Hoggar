using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    public float destroyTime = 1f;  // Time in seconds to destroy the particle
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);  // Destroy the particle after destroyTime seconds
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
