using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
  
    //this is attached to each object that is interactable - can be split into diff scripts or as one for all interact objects

    public void InteractMethod()
    {
        Debug.Log("Interaction Works");
        Destroy(gameObject);
    }

}
