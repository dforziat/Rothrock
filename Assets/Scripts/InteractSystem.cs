using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractSystem : MonoBehaviour
{
    [SerializeField] int rayLength = 10;
    [SerializeField] LayerMask InteractObjs;
    [SerializeField] string excludeLayerName = null;
    [SerializeField] KeyCode interactKey = KeyCode.E;

    [SerializeField] private Image crosshair = null;
    bool isCrosshairActive;
    bool doOnce;

    PickupController raycastedObj;

    const string interactableTag = "InteractiveObject";

    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | InteractObjs.value;

        if(Physics.Raycast(transform.position, fwd, out hit, rayLength, mask))
        {
            //maybe use different tags for diff interact objs to tell which to do? could scale well
            if (hit.collider.CompareTag(interactableTag))
            {
                if(!doOnce)
                {
                    raycastedObj = hit.collider.gameObject.GetComponent<PickupController>();
                    CrosshairChange(true);
                }
                isCrosshairActive = true;
                doOnce = true;

                if(Input.GetKeyDown(interactKey))
                {
                    //calls a method from another script
                    raycastedObj.InteractMethod();
                }
            }
        }

        else
        {
            if(isCrosshairActive)
            {
                CrosshairChange(false);
                doOnce = false;
            }
        }
    }
    void CrosshairChange(bool on)
    {
        if (on && !doOnce)
        {
            crosshair.color = Color.red;
        }
        else
        {
            crosshair.color = Color.white;
        }
    }
}
