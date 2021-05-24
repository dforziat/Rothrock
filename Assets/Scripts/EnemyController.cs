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

    [Header("Movement")]
    public float moveSpeed;
    public float attackRange;
    public float yPathOffset;

    [SerializeField] float aggroDistance = 20;
    [SerializeField] float aggroDistanceMax = 30;
    bool aggroToggle;
    float distToPlayer;

    // TODO: add "weapon" = hands essentially

    private GameObject target;
    private NavMeshAgent navMeshAgent; 

    void Start()
    {
        // get the compnents
        // weapon = GetComponent<Weapon>();
        target = GameObject.FindWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        AggroCheck();
        ChaseTarget();
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

    void Die()
    {
        Destroy(gameObject);
    }

    void AggroCheck()
    {
        distToPlayer = Vector3.Distance(transform.position, target.transform.position);

        if (distToPlayer <= aggroDistance)
        {
            aggroToggle = true;
        }

        if (aggroToggle == true && distToPlayer > aggroDistanceMax)
        {
            aggroToggle = false;
        }

    }

}
