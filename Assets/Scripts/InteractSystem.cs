using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractSystem : MonoBehaviour
{
    [SerializeField] int rayLength = 10;
    [SerializeField] LayerMask InteractObjs;
    [SerializeField] string excludeLayerName = null;
    [SerializeField] KeyCode interactKey = KeyCode.E;

    [SerializeField] private Image crosshair = null;
    [SerializeField] TextMeshProUGUI interactText;

    PickupController raycastedObj;
    HealthPickup healthPickup;

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
                // raycastedObj = hit.collider.gameObject.GetComponent<PickupController>();
                 healthPickup = hit.collider.gameObject.GetComponent<HealthPickup>();
                 InteractPopUp(true);

                if(Input.GetKeyDown(interactKey))
                {
                    //calls a method from another script
                   // raycastedObj.InteractMethod();
                    healthPickup.HealPickup();


                }
            }
        }

        else
            InteractPopUp(false);
         
    }


    void InteractPopUp(bool on)
    {
        if (on)
        {
            interactText.gameObject.SetActive(true);
        }
        else
        {
            interactText.gameObject.SetActive(false);
        }
    }

}


