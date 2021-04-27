using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandgunAmmoPickup : MonoBehaviour
{
    int handgunAmmoSize = 10;
    private void OnTriggerEnter(Collider other)
    {
        //this should change to a GUI and button press in the future
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Plaer collided with ammo");
            Inventory playerInventory = other.gameObject.GetComponent<Inventory>();
            if (!playerInventory.isHandgunAmmoFull())
            {
                playerInventory.increaseHandgunAmmo(handgunAmmoSize);
                Destroy(gameObject);
            }
           // GameUI.instance.UpdateHealthKitText(playerInventory.getHealthKits(), playerInventory.getHealthKitsMax());
        }
    }
}
