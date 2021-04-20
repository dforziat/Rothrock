using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        //this should change to a GUI and button press in the future
        if (other.gameObject.tag == "Player")
        {
            Inventory playerInventory = other.gameObject.GetComponent<Inventory>();
            if(playerInventory.healthKits < playerInventory.healthKitsMax)
            {
                other.gameObject.GetComponent<Inventory>().healthKits += 1;
                Destroy(gameObject);
            }
            
        }
    }
}
