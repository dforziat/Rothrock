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

    }

    public void aimDownSights()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, aimDownSight, aimSpeed * Time.deltaTime);
    }

    public void returnToHip()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, hipFire, aimSpeed * Time.deltaTime);
    }
}
