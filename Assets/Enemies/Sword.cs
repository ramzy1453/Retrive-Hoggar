using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    private Collider collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        Health health = other.gameObject.GetComponent<Health>();
        if (health != null && health.gameObject.CompareTag("Player"))
        {
            health.TakeDamage(3f);
        }
        collider.isTrigger = false;
    }
}
