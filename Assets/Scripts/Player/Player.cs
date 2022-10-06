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
            print("Player Died");
            Main.GameManager.PlayerDeath(gameObject.GetComponent<PlayerController>());
            return;
        }
        
        health -= damage;
        print($"Player Survived. health left: {health}");
    }

    public void DrainStamina(float stamina)
    {
        this.stamina -= stamina;
    }
    
}
