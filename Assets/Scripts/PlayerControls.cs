using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] float runSpeed;

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
    Vector3 vel;
    [SerializeField] CharacterController controller;


    void Awake()
    {
        // get the camera
        cam = Camera.main;

        //disable cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Gravity();
        CamLook();
        Movement();
    }

    void Movement()
    {
        //this can be done better + need to make it case running doesnt work while ADS, ADS needs slow walk
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("Running");
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 dir = transform.right * x + transform.forward * z;
            controller.Move(dir * runSpeed * Time.deltaTime);
        }
        else
        {
            Debug.Log("Walking");
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 dir = transform.right * x + transform.forward * z;
            controller.Move(dir * moveSpeed * Time.deltaTime);
        }

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




}
