using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Component References
    [Header("Player")] 
    
    public Transform orientation; //this
    public Transform playerObj;
    public Rigidbody rb;

    [Header("Camera")] 
    public Transform GameCamera;

    public GameObject VirtualCam;
    
    
    
    //fields Public

    public bool Selected = false;
    public float moveSpeed = 10f;
    public float rotationSpeed = 7f;

    public float JumpForce = 10f;

    
    //fields Private
    private Vector2 movementInput;
    private Vector3 moveDirection;
    
    
    //  Start / Update
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //sets the view and move directions (Camera and orientation of character)
        if (Selected)
        {
            SetViewDirection();
            SetMoveDirection();
        }
    }

    private void FixedUpdate()
    {
        if (Selected)
        {
            AddMoveForce();
        }
    }

    

    
    
    
    
    
    // Movement
    
    public void UpdateMoveVec(Vector2 movementInput)
    {
        print($"Movment {movementInput}");
        this.movementInput = movementInput;
    }

    public void AddMoveForce()
    {
        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
    }

    public void SetMoveDirection()
    {
        moveDirection = orientation.forward * movementInput.y + orientation.right * movementInput.x;

        if (moveDirection != Vector3.zero)
            playerObj.forward = Vector3.Slerp(playerObj.forward, moveDirection.normalized, Time.deltaTime * rotationSpeed);
    }
    
    // Camera

    public void SetViewDirection()
    {
        Vector3 viewDir = transform.position - new Vector3(GameCamera.position.x, transform.position.y, GameCamera.position.z);
        orientation.forward = viewDir.normalized;
    }
    
    
    
    
    
    
    
    
    //otherActions
    
    public void Jump()
    {
        print($"Jump");
        rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
    }

    public void Shoot()
    {
        print("Shoot");
    }

    public void SwitchCamera()
    {
        // SetActiveCamera();
        // EnableCamera();
    }

    public void SwitchWeapon()
    {
        print("Switching weapon");
    }

    public void Aim()
    {
        print("aiming");
    }
}
