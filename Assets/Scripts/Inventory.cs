using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Inventory")]
    [SerializeField] private int healthKits = 0;
     private int healthKitsMax = 3;
    [SerializeField]  int handgunAmmo = 10;
     int handgunAmmoMax = 20;
    public HealthPickup healthPickup; 

    void Start()
    {
        healthPickup = new HealthPickup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increaseHealthKits(int numHealthKits)
    {
        if (healthKits < healthKitsMax)
        {
            this.healthKits += numHealthKits;
        }
    }

    public int getHealthKits()
    {
        return healthKits;
    }

    public void setHealthKits(int healthKits)
    {
        this.healthKits = healthKits;
    }

    public int getHealthKitsMax()
    {
        return healthKitsMax;
    }

    public void setHealthKitsMax(int healthKitsMax)
    {
        this.healthKitsMax = healthKitsMax;
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
