using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //References
    
    [Header("Camera References")] 
    public Transform GameCamera;
    
    public GameObject FreeVCam;
    
    public GameObject AimVCam;

    public Transform AimLookAt;
    
    public Transform AimFollow;
    
    [Header("Player References")] 
    
    [SerializeField]
    private Transform orientation;
    
    [SerializeField] 
    public Transform playerObj;
    
    [SerializeField]
    private Rigidbody rb;

    [SerializeField] 
    private WeaponController weaponController;
    
    
    
    [Header("CameraFields")] 
    
    [SerializeField]
    private CameraMode cameraMode = CameraMode.FreeLook;
    
    
    
    [Header("MovementFields")] 
    
    public bool Selected = false;
    
    [SerializeField] 
    private float TopSpeed = 0.5f;
    
    [SerializeField] 
    private float topSpeedTime = 2f;
    
    [SerializeField] 
    private AnimationCurve accelerationCurve;
    
    private float timer = 0;
    
    [SerializeField] 
    private float rotationSpeed = 16f;

    [SerializeField] 
    private float JumpForce = 2f;
    
    
    private Vector2 movementInput;
    private Vector3 moveDirection;
    

    

    

    
    // Update / start

    private void Start()
    {
        weaponController = GetComponent<WeaponController>();
    }

    private void Update()
    {
        //sets the view and move directions (Camera and orientation of character)
        if (Selected)
        {
            switch (cameraMode)
            {
                case CameraMode.FreeLook:
                    SetDirectionFree();
                    break;
                
                case CameraMode.Aim:
                    SetDirectionAim();
                    weaponController.AimWeapon(GameCamera);
                    break;
            }
        }

        timer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (Selected)
        {
            SetMoveSpeed();
        }
        
    }

    

    
    
    // Movement
    
    public void UpdateMoveVec(Vector2 movementInput)
    {
        print($"Movment {movementInput}");

        //resetTimer
        if (movementInput == Vector2.zero ||
            (this.movementInput == Vector2.zero && movementInput != Vector2.zero))
        {
            timer = 0;
            print("timerReset");
        }
        
        this.movementInput = movementInput;
    }

    private void SetMoveSpeed()
    {
        Vector3 newMovement = moveDirection.normalized * (TopSpeed * accelerationCurve.Evaluate(timer / topSpeedTime));
        newMovement.y = rb.velocity.y;
        
        rb.velocity = newMovement;
    }


    private void SetDirectionFree()
    {
        Vector3 viewDir = transform.position - new Vector3(GameCamera.position.x, transform.position.y, GameCamera.position.z);
        orientation.forward = viewDir.normalized;
        
        moveDirection = orientation.forward * movementInput.y + orientation.right * movementInput.x;

        if (moveDirection != Vector3.zero)
            playerObj.forward = Vector3.Slerp(playerObj.forward, moveDirection.normalized, Time.deltaTime * rotationSpeed);
    }
    private void SetDirectionAim()
    {
        Vector3 viewDir = AimLookAt.position - new Vector3(GameCamera.position.x, AimLookAt.position.y, GameCamera.position.z);
        orientation.forward = viewDir.normalized;
        
        
        moveDirection = orientation.forward * movementInput.y + orientation.right * movementInput.x;

        playerObj.forward = viewDir.normalized;
    }

    
    
    
    
    
    //Action functions
    
    public void Jump()
    {
        print($"Jump");
        rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
    }

    public void Shoot()
    {
        weaponController.CurrentWeaponRef.Shoot();
        //print("Shoot");
    }

    public void SwitchCamera()
    {
        switch (cameraMode)
        {
            case CameraMode.FreeLook:
                cameraMode = CameraMode.Aim;
                FreeVCam.SetActive(false);
                AimVCam.SetActive(true);
                break;

            case CameraMode.Aim:
                cameraMode = CameraMode.FreeLook;
                FreeVCam.SetActive(true);
                AimVCam.SetActive(false);
                break;
        }
    }

    public void SwitchWeapon()
    {
        print("Switching weapon");
    }

    public void Aim()
    {
        print("aiming");
        SwitchCamera();
    }


    //CameraRelated Functions

    public void EnableCamera()
    {
        switch (cameraMode)
        {
            case CameraMode.FreeLook:
                FreeVCam.SetActive(true);
                AimVCam.SetActive(false);
                break;

            case CameraMode.Aim:
                FreeVCam.SetActive(false);
                AimVCam.SetActive(true);
                break;
        }
    }

    public void DisableCamera()
    {
        FreeVCam.SetActive(false);
        AimVCam.SetActive(false);
    }
    
    public enum CameraMode
    {
        FreeLook,
        Aim
    }
    
}
