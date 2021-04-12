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

    void Start()
    {
        // get the compnents
        // weapon = GetComponent<Weapon>();
        target = GameObject.FindWithTag("Player");

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
        if (path.Count == 0)
            return;

        //move towards closest path
        transform.position = Vector3.MoveTowards(transform.position, path[0] + new Vector3(0, yPathOffset, 0), moveSpeed * Time.deltaTime);

        if (transform.position == path[0] + new Vector3(0, yPathOffset, 0))
            path.RemoveAt(0);
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
