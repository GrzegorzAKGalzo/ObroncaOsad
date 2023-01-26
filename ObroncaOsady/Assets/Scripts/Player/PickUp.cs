using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnCollisionTonEnter(Collision collision)
    {
        Debug.Log("You have picked up " + collision.gameObject.name);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Debug.Log(other.gameObject.name);
            Destroy(other.gameObject);
        }
    }
}
