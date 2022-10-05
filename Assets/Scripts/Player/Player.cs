using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour
{
    public int playerID;
    public int teamID;
    public float health = 100f;
    public float stamina = 100f;


    public void TakeDamage(float damage)
    {
        
        if ((health -= damage) <= 0)
        {
            //die
            Main.GameManager.PlayerDeath(playerID);
            return;
        }
        health -= damage;
    }

    public void DrainStamina(float stamina)
    {
        this.stamina -= stamina;
    }
    
}
