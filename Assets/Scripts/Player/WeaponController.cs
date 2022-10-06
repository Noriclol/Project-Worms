using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon References")] 
    [SerializeField]
    private Gun Pistol;
    
    [SerializeField]
    private Gun Rifle;
    
    [SerializeField]
    private Gun ShotGun;
    
    [SerializeField]
    private Gun Sniper;

    [Header("GunSpawner Ref")] 
    public GameObject GunPickup;

    [Header("Current Inventory")]
    public Gun Primary;
    public Gun Secondary;

    [Header("Handle Transforms")]
    public Transform ActiveWeapon;
    public Transform InactiveWeapon;

    private Transform BulletSpawnPosition;

    
    
    private GunSelection CurrentWeapon = GunSelection.secondary;
    public Gun CurrentWeaponRef;

    [SerializeField] 
    private float AimAngleMult = 2f;

    public bool WeaponLock = false;
    
    public event Action EventWeaponInit = delegate {};

    
    public void Start()
    {
        EquipStarterLoadout();
        //EventWeaponInit();
    }

    public void AimWeapon(Transform cameraTransform)
    {
        var newRotation = new Quaternion(cameraTransform.rotation.x * AimAngleMult, 0, 0, 1);
        ActiveWeapon.localRotation = newRotation;
    }

    public void Shoot()
    {
        //Debug.Log($"{GunPrefab.name} Shoot");
        if (!WeaponLock)
            WeaponLock = true;
        
        SpawnBullet();
        //do effects
    }
    
    private void SpawnBullet()
    {
        for (int i = 0; i < CurrentWeaponRef.BulletsPerShot; i++)
        {
            //Instantiate a bulletInstance
            var newBullet = Instantiate(CurrentWeaponRef.BulletPrefab, BulletSpawnPosition.position, BulletSpawnPosition.rotation);

            //apply correct force and direction to bullet
            var bulletscript = newBullet.GetComponent<Bullet>();
            bulletscript.damage = CurrentWeaponRef.damage;
            bulletscript.rb.AddForce(BulletSpawnPosition.forward * CurrentWeaponRef.MuzzleVelocity, ForceMode.VelocityChange);
        }
    }
    
    
    
    
    
    
    // public void Equip(GunSelection slot, Gun gun)
    // {
    //     //try to put new equipment in primary
    //     
    //     //primary available
    //     if (!Primary && Secondary)
    //     {
    //         Primary = gun;
    //         return;
    //     }
    //     SwitchSelected();
    // }
    

    private void UpdateWeapons()
    {
        //destroy Guns

        if (ActiveWeapon.childCount > 0)
            GameObject.Destroy(ActiveWeapon.GetChild(0).gameObject);

        if (InactiveWeapon.childCount > 0)
            GameObject.Destroy(InactiveWeapon.GetChild(0).gameObject);


        GameObject Active;
        GameObject Inactive;


        //InstantiateGuns at location
        switch (CurrentWeapon)
        {
            case GunSelection.primary:
                //Instantiate
                Active   = Instantiate(Primary.GunPrefab, ActiveWeapon);
                Inactive = Instantiate(Secondary.GunPrefab, InactiveWeapon);
                //Attach
                Active.transform.SetParent(ActiveWeapon.transform, false);
                Inactive.transform.SetParent(InactiveWeapon.transform, false);

                BulletSpawnPosition = Active.transform.GetChild(0);
                CurrentWeaponRef = Primary;
                
                break;
            
            
            case GunSelection.secondary:
                //Instantiate
                Active = Instantiate(Secondary.GunPrefab, ActiveWeapon);
                Inactive = Instantiate(Primary.GunPrefab, InactiveWeapon);
                //Attach
                Active.transform.SetParent(ActiveWeapon.transform, false);
                Inactive.transform.SetParent(InactiveWeapon.transform, false);
                
                BulletSpawnPosition = Active.transform.GetChild(0);
                CurrentWeaponRef = Secondary;
                break;
        }
    }

    
    
    [ContextMenu("Switch Gun")]
    public void SwitchSelected()
    {

        if (WeaponLock)
        {
            return;
        }
        
        switch (CurrentWeapon)
        {
            case GunSelection.primary:
                CurrentWeapon = GunSelection.secondary;
                break;


            case GunSelection.secondary:
                CurrentWeapon = GunSelection.primary;
                break;
        }
        UpdateWeapons();
    }

    private void EquipStarterLoadout()
    {
        Secondary = Pistol;
        
        int randomGun = Random.Range(1, 4);
        switch (randomGun)
        {
            case 1:
                Primary = Rifle;
                break;
            case 2:
                Primary = ShotGun;
                break;
            case 3:
                Primary = Sniper;
                break;
            case 4:
                Primary = Sniper;
                break;
        }
        
        //CurrentWeaponRef = Secondary;
        
        UpdateWeapons();
    }

    public enum GunSelection
    {
        primary,
        secondary
    }
    
    
}
