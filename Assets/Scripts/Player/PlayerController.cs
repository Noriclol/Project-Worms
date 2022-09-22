using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Camera
    public GameObject ThirdPerson;
    public GameObject FirstPerson;
    public CameraMode cameraMode = CameraMode.Third;


    private void Start()
    {
        EnableCamera();
    }

    
    
    
    
    
    //EventActionFunctions

    public void Move(Vector2 movementInput)
    {
        print($"Movment {movementInput}");
    }
    
    public void Jump()
    {
        print($"Jump");
    }

    public void Shoot()
    {
        print("Shoot");
    }

    public void SwitchCamera()
    {
        SetActiveCamera();
        EnableCamera();
    }

    public void SwitchWeapon()
    {
        print("Switching weapon");
    }

    public void Aim()
    {
        print("aiming");
    }

    
    //~EventActionFunctions
    
    
    
    //Minor Functions
    
    private void EnableCamera()
    {
        if (cameraMode == CameraMode.First)
        {
            FirstPerson.SetActive(true);
            ThirdPerson.SetActive(false);
        }
        if (cameraMode == CameraMode.Third)
        {
            FirstPerson.SetActive(false);
            ThirdPerson.SetActive(true);
        }
    }
    
    private void DisableCamera()
    {
        ThirdPerson.SetActive(false);
        ThirdPerson.SetActive(false);
    }

    private void SetActiveCamera()
    {
        switch (cameraMode)
        {
            case CameraMode.First:
                cameraMode = CameraMode.Third;
                break;
            
            case CameraMode.Third:
                cameraMode = CameraMode.First;
                break;
        }
    }
}
