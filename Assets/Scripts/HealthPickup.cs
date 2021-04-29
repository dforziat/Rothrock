using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 50;


    private void OnTriggerEnter(Collider other)
    {
        //this should change to a GUI and button press in the future
        if (other.gameObject.tag == "Player")
        {
            Inventory playerInventory = other.gameObject.GetComponent<Inventory>();
            if(playerInventory.getHealthKits() < playerInventory.getHealthKitsMax())
            {
                playerInventory.setHealthKits(playerInventory.getHealthKits() + 1);
                Destroy(gameObject);
            }
            GameUI.instance.UpdateHealthKitText(playerInventory.getHealthKits(), playerInventory.getHealthKitsMax());
        }
    }

    //method version
    public void HealPickup()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Inventory playerInventory = player.gameObject.GetComponent<Inventory>();

        if (playerInventory.getHealthKits() < playerInventory.getHealthKitsMax())
            {
                playerInventory.setHealthKits(playerInventory.getHealthKits() + 1);
                Destroy(gameObject);
            }
            GameUI.instance.UpdateHealthKitText(playerInventory.getHealthKits(), playerInventory.getHealthKitsMax());
        
    }

}
