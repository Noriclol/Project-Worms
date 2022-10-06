using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    public Collider collider;
    public float damage;
    private void OnCollisionEnter(Collision other)
    {
        var hitTag = other.gameObject.tag;

        switch (hitTag)
        {
            case "Map":
                Destroy(gameObject);
                break;
            
            
            case "Player":
                
                print("Hit Player");
                
                var player = other.gameObject.GetComponent<Player>();
                player.TakeDamage(damage);
                
                Destroy(gameObject);
                break;
        }
        
    }
}
