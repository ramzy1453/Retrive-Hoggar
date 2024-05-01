using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    public GameObject swordOn;
    public GameObject swordOff;
    private bool swordOut = false;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private float attackDistance;
    private Transform target;
    public Collider swordCollider;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            if(Vector3.Distance(transform.position, target.position) < attackDistance)
            {
                Attack();
            }   
        }
    }

    public void Attack()
    {
        transform.LookAt(target);
        animator.SetTrigger("Attack");
        swordCollider.isTrigger = true;
    }


    public void StartAttack(Collider navTarget)
    {
        if(!swordOut) {
            animator.SetTrigger("Attack0");
            swordOff.SetActive(false);
            swordOn.SetActive(true);
            swordOut = true;
        }
        target = navTarget.transform;
        animator.SetBool("Run", true);
        navMeshAgent.SetDestination(navTarget.transform.position);

    }
    

    public void ReceiveDamage()
    {
        animator.SetTrigger("Hit");
        
    }
    public void Die()
    {
        animator.SetTrigger("Die");
        GameManager.instance.CheckWin();
        Destroy(gameObject, 3f);
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward * attackDistance, Color.red);
        
    }
}
