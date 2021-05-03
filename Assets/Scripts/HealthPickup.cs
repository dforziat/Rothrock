using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 50;

    //method version
    public void HealPickup()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Inventory playerInventory = player.gameObject.GetComponent<Inventory>();

        if (!playerInventory.isHealthKitFull())
        {
            playerInventory.increaseHealthKits();
            Destroy(gameObject);
        }
        GameUI.instance.UpdateHealthKitText(playerInventory.healthKits, playerInventory.healthKitsMax);
        
    }

}
