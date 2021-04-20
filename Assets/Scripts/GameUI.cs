using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    //This is where inventory stuff is stored as well as UI representation of it
 
    [Header("HUD")]
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI healthText;

    public static GameUI instance;

    void Awake()
    {
        instance = this;
    }
    
    public void UpdateAmmoText(int currentAmmo, int maxAmmoCapacity)
    {
        ammoText.text = "Ammo: " + currentAmmo + "/" + maxAmmoCapacity;
    }

    public void UpdateHealthText(int currentHp, int maxHp)
    {
        healthText.text = "HP: " + currentHp + "/" + maxHp;
    }

}
