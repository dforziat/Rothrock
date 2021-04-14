using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    [Header("Stats")]
    public int curHp;
    public int maxHp;

    [Header("Movement")]
    public float moveSpeed;
    public float attackRange;
    public float yPathOffset;

    private List<Vector3> path;

    // TODO: add "weapon" = hands essentially

    private GameObject target;
    private NavMeshAgent navMeshAgent; 

    void Start()
    {
        // get the compnents
        // weapon = GetComponent<Weapon>();
        target = GameObject.FindWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        InvokeRepeating("UpdatePath", 0.0f, .5f);
    }

    void Update()
    {

        float dist = Vector3.Distance(transform.position, target.transform.position);

        if (dist <= attackRange)
        {
           // if (weapon.CanShoot())
              //  weapon.shoot();
        }
        else
        {
            ChaseTarget();
        }

        //look at target
        Vector3 dir = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

        transform.eulerAngles = Vector3.up * angle;

    }

    void ChaseTarget()
    {
        navMeshAgent.SetDestination(target.transform.position);
    }



    void UpdatePath()
    {
        //calc path to target
        NavMeshPath navMeshPath = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, navMeshPath);

        //saving as a list
        path = navMeshPath.corners.ToList();
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
}
