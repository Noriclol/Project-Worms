using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    public Collider collider;

    private void OnCollisionEnter(Collision other)
    {
        var hitTag = other.gameObject.tag;

        switch (hitTag)
        {
            case "Map":
                //Destroy(this);
                break;
            case "Player":
                print("Hit Player");
                Destroy(this);
                break;
        }
        
    }
}
