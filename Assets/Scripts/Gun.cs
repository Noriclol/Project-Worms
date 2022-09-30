using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newGun", menuName = "Weapons/Gun")]
public class Gun : ScriptableObject
{
    [Header("References")]
    public GameObject GunPrefab;
    private Transform BulletSpawnPosition;


    public void OnValidate()
    {
        //BulletSpawnPosition = GunPrefab.
    }

    public void Shoot()
    {
        
    }
    
    private void SpawnBullet()
    {
        
    }
    
}
