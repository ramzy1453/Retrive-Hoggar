
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class FieldOfView : MonoBehaviour
{
    public List<Transform> visibleObjects;

    [SerializeField] private float _viewRadius = 6f;
    [SerializeField] private float _viewAngle = 30f;
    [SerializeField] private LayerMask _blockingLayers;
    private EnemyController enemy;

    void Start()
    {
        enemy = GetComponent<EnemyController>();
    }

    void Update()
    {
        visibleObjects.Clear();

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, _viewRadius);
       
        foreach (Collider target in targetsInViewRadius)
        {
            // Check if the detected creature is from a different team
            if (target.CompareTag("Player"))
            {
                Vector3 directionToTarget = (target.transform.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < _viewAngle)
                {
                    Vector3 headPos = transform.position + Vector3.up; // Assuming head position is at the center of the GameObject
                    Vector3 targetHeadPos = target.transform.position + Vector3.up; // Assuming head position is at the center of the GameObject

                    Vector3 dirToTargetHead = (targetHeadPos - headPos).normalized;
                    float distToTargetHead = Vector3.Distance(headPos, targetHeadPos);

                    if (Physics.Raycast(headPos, dirToTargetHead, distToTargetHead, _blockingLayers))
                    {
                        continue;
                    }

                    visibleObjects.Add(target.transform);
                   
                    enemy.StartAttack(target);

                }
            }
        }

        
    }

}
