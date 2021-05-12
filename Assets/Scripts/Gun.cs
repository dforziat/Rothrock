using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    float damage = 1f;
    float range = 100f;
    public int maxAmmoCapacity = 6;
    public int currentAmmo = 6;

    [SerializeField] Camera cam;

    // Update is called once per frame
    void Update()
    {
        if (GameUI.instance.GameIsPaused == true)
            return;
        else
        {
            // Add a check for fireSpeed here as well, so player doesn't blast as a fast as they can click
            if (Input.GetButtonDown("Fire1") && currentAmmo > 0)
            {
                Shoot();

            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }
        }
    }


    void Shoot()
    {
        RaycastHit hit;
        //Physics.Raycast(ORIGIN, DIRECTION, INFO ON HIT, RANGE) 
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log("shoot");
            Debug.Log(hit.transform.name);
        }
        currentAmmo--;
        GameUI.instance.UpdateAmmoText(currentAmmo, maxAmmoCapacity);

    }

    void Reload()
    {
        Inventory inventory = gameObject.GetComponent<Inventory>();
        if(inventory.handgunAmmo < maxAmmoCapacity)
        {
            currentAmmo = inventory.handgunAmmo;
            inventory.handgunAmmo = 0;
        }
        else
        {
            int ammoInMagzine = maxAmmoCapacity - currentAmmo;
            currentAmmo = maxAmmoCapacity;
            inventory.handgunAmmo -= ammoInMagzine;
        }
        GameUI.instance.UpdateAmmoText(currentAmmo, maxAmmoCapacity);
    }
}
