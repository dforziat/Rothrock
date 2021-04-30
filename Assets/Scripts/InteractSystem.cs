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
    [SerializeField] TextMeshProUGUI interactText;


    HealthPickup healthPickup;

    const string hpTag = "InteractableHealthKit";
    const string ammoTag = "InteractableAmmo";


    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | InteractObjs.value;

        if(Physics.Raycast(transform.position, fwd, out hit, rayLength, mask))
        {
            //copy and replace tag + method to scale
            if (hit.collider.CompareTag(hpTag))
            {
                healthPickup = hit.collider.gameObject.GetComponent<HealthPickup>();
                //interactText.text = "CHANGE TEXT IF NEEDDED";
                InteractPopUp(true);

                if(Input.GetKeyDown(interactKey))
                    healthPickup.HealPickup();




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


