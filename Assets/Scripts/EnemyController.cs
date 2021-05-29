using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    [Header("Stats")]
    public int curHp = 5;
    public int maxHp = 5;
    int stopDist = 2;
    [SerializeField] int damage = 5;

    [Header("Movement")]
    public float moveSpeed;
    float attackRange = 3;
    public float yPathOffset;

    [SerializeField] float aggroDistance = 20;
    [SerializeField] float aggroDistanceMax = 30;
    bool aggroToggle;
    float distToPlayer;

    

    private GameObject target;
    private NavMeshAgent navMeshAgent;

    public PlayerControls playerControls;

    void Start()
    {
        target = GameObject.FindWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        AggroCheck();
        ChaseTarget();
        Attack();
    }

    void ChaseTarget()
    {
        if (aggroToggle == true)
        {
            navMeshAgent.SetDestination(target.transform.position);

            //look at target
            Vector3 dir = (target.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            transform.eulerAngles = Vector3.up * angle;
        }
    }


    public void TakeDamage(int damage)
    {
        curHp -= damage;
        if (curHp <= 0)
            Die();
    }

    void Attack()
    {
        if (distToPlayer <= attackRange)
        {
            //needs 'downtime" like gunshot, can we do animation to do that?
            playerControls.TakeDamage(damage);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void AggroCheck()
    {
        distToPlayer = Vector3.Distance(transform.position, target.transform.position);

        if ((distToPlayer <= aggroDistance) && (distToPlayer > stopDist))
        {
            aggroToggle = true;
        }

        if (aggroToggle == true && ((distToPlayer > aggroDistanceMax) || (distToPlayer < stopDist)))
        {
            aggroToggle = false;
        }

    }

}
