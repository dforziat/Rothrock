using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [Header("HUD")]
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI healthKitText;

    public static GameUI instance;

    public bool GameIsPaused;
    public GameObject InventoryMenu;

    public Inventory playerInventory;

    int curHealthKits;

    void Awake()
    {
        instance = this;
        GameIsPaused = false;
    }

    void Update()
    {
        TogglePause();

        //UpdateInventory(); can we integrate this into the other methods?

    }

    public void UpdateAmmoText(int currentAmmo, int maxAmmoCapacity)
    {
        ammoText.text = "Ammo: " + currentAmmo + "/" + maxAmmoCapacity;
    }

    public void UpdateHealthText(int currentHp, int maxHp)
    {
        healthText.text = "HP: " + currentHp + "/" + maxHp;
    }

    public void UpdateHealthKitText(int currentHealthKits, int maxHealthKits)
    {
        healthKitText.text = "HealthKits: " + currentHealthKits + "/" + maxHealthKits;
    }

    void Resume()
    {
        InventoryMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        GameIsPaused = false;

    }
    void Pause()
    {
        InventoryMenu.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        GameIsPaused = true;
    }

    void TogglePause()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    void UpdateInventory()
    {
        //get curHealthkits
        curHealthKits = playerInventory.healthKits;

        //get cur ammo
    }

}
