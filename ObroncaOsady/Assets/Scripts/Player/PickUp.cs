using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public playerMovement backpack;
    private void OnCollisionTonEnter(Collision collision)
    {
        Debug.Log("You have picked up " + collision.gameObject.name);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "rock")
        {
            backpack.addItem("rock");
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "wood")
        {
            backpack.addItem("wood");
            Destroy(other.gameObject);
        }
    }
}
