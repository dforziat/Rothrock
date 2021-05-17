using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
 
    [Header("Inventory")]
    [SerializeField] public int healthKits = 0;
     public int healthKitsMax = 3;
    [SerializeField] public int handgunAmmo = 6;
    public int handgunAmmoMax = 36;
    public HealthPickup healthPickup; 

    void Start()
    {
        healthPickup = new HealthPickup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increaseHealthKits()
    {
        if (healthKits < healthKitsMax)
        {
            this.healthKits++; ;
        }
    }

    public void useHealthKit()
    {
        if(healthKits > 0)
        {
            healthKits--;
        }
    }

    public bool isHealthKitFull()
    {
        if (healthKits == healthKitsMax)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

  
    public void increaseHandgunAmmo(int pickupHandgunAmmo)
    {
        if(pickupHandgunAmmo + handgunAmmo > handgunAmmoMax) //only pickup ammo if there is some space for a partial amount
        {
            this.handgunAmmo = handgunAmmoMax;
        }else{
            this.handgunAmmo += pickupHandgunAmmo;
        }
    }

    public bool isHandgunAmmoFull()
    {
        if(handgunAmmo == handgunAmmoMax)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



}
