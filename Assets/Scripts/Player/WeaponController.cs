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

    private GunSelection CurrentWeapon = GunSelection.secondary;
    public Gun CurrentWeaponRef;
    private int currentShots = 0;

    [SerializeField] 
    private float AimAngleMult = 2f;
    

    public void Start()
    {
        EquipStarterLoadout();
        UpdateWeapons();
    }

    public void AimWeapon(Transform cameraTransform)
    {
        // ActiveWeapon.localRotation.Set
        // (
        //     cameraTransform.rotation.x, 
        //     ActiveWeapon.rotation.y,
        //     ActiveWeapon.rotation.z, 
        //     ActiveWeapon.rotation.w
        // );

        var newRotation = new Quaternion(cameraTransform.rotation.x * AimAngleMult, 0, 0, 1);
        ActiveWeapon.localRotation = newRotation;
        // print($"ActiveWeapon {ActiveWeapon.localRotation}");
        // print($"NewAim {newRotation}");
        // print($"GameCamera {cameraTransform.rotation.x}");
    }

    public void Equip(GunSelection slot, Gun gun)
    {
        //try to put new equipment in primary
        
        //primary available
        if (!Primary && Secondary)
        {
            Primary = gun;
            return;
        }
        SwitchSelected();
    }

    public void ToggleActiveWeapon()
    {
        switch (CurrentWeapon)
        {
            case GunSelection.primary:
                CurrentWeapon = GunSelection.secondary;
                break;
            case GunSelection.secondary:
                CurrentWeapon = GunSelection.primary;
                break;
        }
    }


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
                break;
            case GunSelection.secondary:
                //Instantiate
                Active = Instantiate(Secondary.GunPrefab, ActiveWeapon);
                Inactive = Instantiate(Primary.GunPrefab, InactiveWeapon);
                //Attach
                Active.transform.SetParent(ActiveWeapon.transform, false);
                Inactive.transform.SetParent(InactiveWeapon.transform, false);
                break;
        }
    }

    private void SwitchSelected()
    {

        switch (CurrentWeapon)
        {
            case GunSelection.primary:
                
                break;


            case GunSelection.secondary:

                break;
        }
    }

    private void EquipStarterLoadout()
    {
        Secondary = Pistol;

        int randomGun = Random.Range(1, 3);
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
        }

        CurrentWeaponRef = Secondary;
    }

    public enum GunSelection
    {
        primary,
        secondary
    }
    
    
}
