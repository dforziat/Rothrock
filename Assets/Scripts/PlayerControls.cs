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

    Camera cam;
    Gun gun;

    Vector3 vel;
    [SerializeField] CharacterController controller;
    public GameObject flashlight;
    bool flashlightToggle = false;

    void Awake()
    {
        // get the components
        cam = Camera.main;
        gun = GetComponent<Gun>();

        //disable cursor
        Cursor.lockState = CursorLockMode.Locked;

        //starts game with flashlight off
        flashlight.SetActive(flashlightToggle);

        //initialize UI
        GameUI.instance.UpdateAmmoText(gun.currentAmmo, gun.maxAmmoCapacity);

    }

    void Update()
    {
        Gravity();
        CamLook();
        Movement();
        Flashlight();
        
    }

    void Movement()
    {
        if(Input.GetMouseButton(1))
            moveSpeed = 0f;
        else if (Input.GetKey(KeyCode.LeftShift))
            moveSpeed = 20f;
        else
            moveSpeed = 5f;

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


}