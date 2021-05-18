using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    float damage = 1f;
    float range = 100f;
    public int maxMagazineCapacity = 6;
    public int currentMagazineAmmo = 6;

    private const string IDLE_STATE = "Idle";
    private const string SHOOTING_STATE = "Shooting";
    private const string RELOAD_STATE = "Reload";

    [SerializeField] Camera cam;

    
    void Update()
    {
        if (GameUI.instance.GameIsPaused == true)
            return;
        else
        {
            Shoot();
            Reload();
        }
    }


    void Shoot()
    {
        // Add a check for fireSpeed here as well, so player doesn't blast as a fast as they can click
        bool isIdle = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(IDLE_STATE);
        if (Input.GetButtonDown("Fire1") && currentMagazineAmmo > 0 && isIdle)
        {
            GetComponent<Animator>().SetTrigger("shoot");
            GetComponent<AudioSource>().Play();
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
            {
                Debug.Log("shoot");
                Debug.Log(hit.transform.name);
            }
            currentMagazineAmmo--;
            GameUI.instance.UpdateAmmoText(currentMagazineAmmo, maxMagazineCapacity);
        }

    }

    void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GetComponent<Animator>().SetTrigger("reload");
            Inventory inventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
            if (inventory.handgunAmmo < maxMagazineCapacity)
            {
                currentMagazineAmmo = inventory.handgunAmmo;
                inventory.handgunAmmo = 0;
            }
            else
            {
                int ammoInMagzine = maxMagazineCapacity - currentMagazineAmmo;
                currentMagazineAmmo = maxMagazineCapacity;
                inventory.handgunAmmo -= ammoInMagzine;
            }
            GameUI.instance.UpdateAmmoText(currentMagazineAmmo, maxMagazineCapacity);
        }
    }
}
