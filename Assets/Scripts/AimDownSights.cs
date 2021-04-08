using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimDownSights : MonoBehaviour
{
    [SerializeField] Vector3 aimDownSight;
    [SerializeField] Vector3 hipFire;
    float aimSpeed = 20;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, aimDownSight, aimSpeed * Time.deltaTime);
        }
        else
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, hipFire, aimSpeed * Time.deltaTime);

        }
    }
}
