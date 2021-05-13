using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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
    [SerializeField] Image hp1;
    [SerializeField] Image hp2;
    [SerializeField] Image hp3;


    void Awake()
    {
        instance = this;
        GameIsPaused = false;
    }

    void Update()
    {
        TogglePause();
        UpdateInventory();
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
        //this is some real jank shit wtf im better than this - but it works?
        if (GameIsPaused == true)
        {
            curHealthKits = playerInventory.healthKits;

            if (curHealthKits == 1)
            {
                hp1.enabled = true;
                hp2.enabled = false;
                hp3.enabled = false;
            }
            else if (curHealthKits == 2)
            {
                hp1.enabled = true;
                hp2.enabled = true;
                hp3.enabled = false;
            }
            else if (curHealthKits == 3)
            {
                hp1.enabled = true;
                hp2.enabled = true;
                hp3.enabled = true;
            }
            else
            {
                hp1.enabled = false;
                hp2.enabled = false;
                hp3.enabled = false;
            }
        }
        else
            return;
    }

}
