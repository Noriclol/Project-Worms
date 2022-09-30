using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Gun Primary;
    public Gun Secondary;


    public void Equip(GunSelection slot, Gun gun)
    {
        
    }
    

    public enum GunSelection
    {
        primary,
        secondary
    }
    
    
}
