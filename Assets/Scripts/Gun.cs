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


    [SerializeField]
    private AudioClip shootSFX;

    [SerializeField]
    private AudioClip reloadSFX;


    [SerializeField] Camera cam;

    
    void Update()
    {
        if (GameUI.instance.GameIsPaused == true)
            return;
        else
        {
            //Shoot();
            Reload();
        }
    }


    public void Shoot()
    {
        bool isIdle = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(IDLE_STATE);
        if (currentMagazineAmmo > 0 && isIdle)
        {
            GetComponent<Animator>().SetTrigger("shoot");
            GetComponent<AudioSource>().PlayOneShot(shootSFX);
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
        bool isIdle = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(IDLE_STATE);
        if (Input.GetKeyDown(KeyCode.R) && isIdle)
        {
            GetComponent<AudioSource>().PlayOneShot(reloadSFX);
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
