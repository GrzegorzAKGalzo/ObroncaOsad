using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
	public WeaponController wc;
	public GameObject HitParticle;
    public int damage;
    public AudioSource treeSound;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Enemy" && wc.isAttacking)
        {
            Debug.Log("AttackRegistered");
            other.GetComponentInParent<Animator>().SetTrigger("gotHit");
            other.GetComponentInParent<WolfNewMovement>().TakeDamage(120);


            //other.GetComponent<Animator>().TakeDamage(damage);
            //Instantiate(HitParticle, new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), other.transform.rotation);
        }
        if (other.tag == "Tree" && wc.isAttacking)
        {

            Debug.Log("treeChop");
            other.GetComponentInParent<treeChop>().takeDamge();
            treeSound.Play();
        }
    }
}
