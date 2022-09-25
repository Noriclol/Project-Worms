using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("References")] 
    public Transform orientation;

    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;


    public float moveSpeed;
    public float rotationSpeed;
    private Vector2 movementInput;
    private Vector3 moveDirection;

    private void OnEnable()
    {
        InputManager.OnMove += UpdateMovementInput;
    }


    private void UpdateMovementInput(Vector2 input)
    {
        movementInput = input;
    }


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        moveDirection = orientation.forward * movementInput.y + orientation.right * movementInput.x;

        if (moveDirection != Vector3.zero)
            playerObj.forward = Vector3.Slerp(playerObj.forward, moveDirection.normalized, Time.deltaTime * rotationSpeed);
        
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
    }
}

