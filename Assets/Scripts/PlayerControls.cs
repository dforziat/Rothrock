using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("Movement")]
    float moveSpeed = 5;

    [Header("Gravity")]
    float grav = -18f;
    [SerializeField] Transform groundCheck;
    float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;

    [Header("Camera")]
    [SerializeField] float lookSensitivity;
    [SerializeField] float maxLookX;
    [SerializeField] float minLookX;
    float rotX;

    [Header("Stats")]
    [SerializeField] int curHp = 50;
    int maxHp = 100;

    Camera cam;
    Gun gun;
    Inventory inventory;
    Animator animator;

    Vector3 vel;
    [SerializeField] CharacterController controller;
    public GameObject flashlight;
    bool flashlightToggle = false;

    public Vector3 dir;

    [Header("Animation States")]
    const string WALK_STATE = "Walk";
    const string ADS_STATE = "ADS";

    void Awake()
    {
        // get the components
        cam = Camera.main;
        gun = GetComponentInChildren<Gun>();
        inventory = GetComponent<Inventory>();
        animator = GetComponent<Animator>();

        //disable cursor
        Cursor.lockState = CursorLockMode.Locked;

        //starts game with flashlight off
        flashlight.SetActive(flashlightToggle);

        //initialize UI
        GameUI.instance.UpdateAmmoText(gun.currentMagazineAmmo, gun.maxMagazineCapacity);
        GameUI.instance.UpdateHealthText(curHp, maxHp);
    }

    void Update()
    {
        if (GameUI.instance.GameIsPaused == true)
            return;
        else
        {
            Gravity();
            CamLook();
            Movement();
            Flashlight();
            useHealthKit();
            shootGun();
            aimDownSights();
        }
    }

    void Movement()
    {
        if (Input.GetMouseButton(1))
        {
            moveSpeed = 0f;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("sprint", true);
            moveSpeed = 20f;
        }
        else
        {
            animator.SetBool("sprint", false);
            moveSpeed = 5f;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 dir = (transform.right * x + transform.forward * z).normalized;
        controller.Move(dir * moveSpeed * Time.deltaTime);
    }

    void CamLook()
    {
        float y = Input.GetAxis("Mouse X") * lookSensitivity;
        rotX += Input.GetAxis("Mouse Y") * lookSensitivity;

        rotX = Mathf.Clamp(rotX, minLookX, maxLookX);

        cam.transform.localRotation = Quaternion.Euler(-rotX, 0, 0);
        transform.eulerAngles += Vector3.up * y;
    }

    void Gravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        vel.y += grav * Time.deltaTime;
        controller.Move(vel * Time.deltaTime);
        if (isGrounded && vel.y < 0)
            vel.y = -2f;
    }

    void Flashlight()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlightToggle = !flashlightToggle;
            flashlight.SetActive(flashlightToggle);
            // Add a cooldown speed here 
        }
    }

    public void TakeDamage(int damage)
    {
        curHp -= damage;
        if (curHp <= 0) { 
        GetComponent<DeathHandler>().HandleDeath();
        }
        GameUI.instance.UpdateHealthText(curHp, maxHp);
    }

    public void Heal(int healAmount)
    {
        if (curHp + healAmount > maxHp)
        {
            curHp = maxHp;
        }
        else
        {
            curHp += healAmount;
        }
        GameUI.instance.UpdateHealthText(curHp, maxHp);
    }

    public void useHealthKit()
    {
        if (Input.GetKeyDown(KeyCode.H)){
            inventory = GetComponent<Inventory>(); //Refresh inventory
            if (inventory.healthKits > 0 && curHp < maxHp)
            {
                Heal(inventory.healthPickup.healAmount);
                inventory.useHealthKit();
                GameUI.instance.UpdateHealthKitText(inventory.healthKits, inventory.healthKitsMax);
            }
            else
            {
                Debug.Log("No health kits");
            }
        }
    }

    public void shootGun()
    {
       bool isWalkState = animator.GetCurrentAnimatorStateInfo(0).IsName(WALK_STATE);
       bool isADSState = animator.GetCurrentAnimatorStateInfo(0).IsName(ADS_STATE);
        if (Input.GetButtonDown("Fire1") && (isWalkState || isADSState))
        {
            gun.Shoot();
        }
    }

    public void aimDownSights()
    {
        bool isWalkState = animator.GetCurrentAnimatorStateInfo(0).IsName(WALK_STATE);
        if(Input.GetMouseButton(1) && isWalkState)
        {
            gun.aimDownSights();
        }
        else
        {
            gun.returnToHip();
        }
    }


}