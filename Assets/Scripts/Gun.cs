using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public Camera cam;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }



    void Shoot()
    {
        RaycastHit hit;
        //Physics.Raycast(ORIGIN, DIRECTION, INFO ON HIT, RANGE) 
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log("shoot");
            Debug.Log(hit.transform.name);
        }
    }
}
