using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;


[CreateAssetMenu(fileName = "newGun", menuName = "Weapons/Gun")]
public class Gun : ScriptableObject
{
    [Header("References")]
    public GameObject GunPrefab;

    public GameObject BulletPrefab;
    
    [Header("Public Fields")]
    public float damage;
    //accuracy in degrees in a cone originating at the bulletspawn
    public float accuracy;
    public int AllowedShots;
    public int BulletsPerShot;
    public float MuzzleVelocity;
    
}
