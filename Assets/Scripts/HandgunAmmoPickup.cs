using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandgunAmmoPickup : MonoBehaviour
{
    int handgunAmmoSize = 10;
 
    public void AmmoPickup()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Inventory playerInventory = player.gameObject.GetComponent<Inventory>();
            if (!playerInventory.isHandgunAmmoFull())
            {
                playerInventory.increaseHandgunAmmo(handgunAmmoSize);
                Destroy(gameObject);
            }
            // GameUI.instance.UpdateHealthKitText(playerInventory.getHealthKits(), playerInventory.getHealthKitsMax());
    }
}
