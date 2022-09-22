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
    

    public void EnableCamera()
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
    
    public void DisableCamera()
    {
        ThirdPerson.SetActive(false);
        ThirdPerson.SetActive(false);
    }

    public void SetActiveCamera()
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
