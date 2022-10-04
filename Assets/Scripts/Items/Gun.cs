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
    
    private Transform BulletSpawnPosition;
    [Header("Public Fields")]
    public float damage;
    //accuracy in degrees in a cone originating at the bulletspawn
    public float accuracy;
    public int AllowedShots;
    public int BulletsPerShot;
    public float MuzzleForce;
    
    
    
    public void OnValidate()
    {
        //BulletSpawnPosition = GunPrefab.
    }

    public void Shoot()
    {
        Debug.Log($"{GunPrefab.name} Shoot");
        SpawnBullet();

        //do effects
    }
    
    private void SpawnBullet()
    {
        //Instantiate a bulletInstance
        //...
        var newBullet = Instantiate(BulletPrefab, BulletSpawnPosition);


        //apply correct force and direction to bullet
        
        //newBullet.GetComponent<Bullet>().rb.AddForce();
        
    }
    


}
