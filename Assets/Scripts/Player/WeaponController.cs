using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon References")] 
    public Gun Pistol;
    public Gun Rifle;
    public Gun ShotGun;
    public Gun Sniper;

    [Header("GunSpawner Ref")] 
    public GameObject GunPickup;

    [Header("Current Inventory")]
    public Gun Primary;
    public Gun Secondary;

    [Header("Handle Transforms")]
    public Transform ActiveWeapon;
    public Transform InactiveWeapon;

    private GunSelection CurrentWeapon = GunSelection.secondary;
    private int currentShots = 0;


    public void Start()
    {
        EquipStarterLoadout();
        UpdateWeapons();
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
        //Equip(GunSelection.secondary, Pistol);
    }

    public enum GunSelection
    {
        primary,
        secondary
    }
    
    
}
