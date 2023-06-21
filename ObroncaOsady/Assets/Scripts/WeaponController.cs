using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Sword;
    public bool canAttack=true;
    public float attackCooldown = 0.3f;
    public AudioClip swordAttacksound;
    public bool isAttacking = false;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (canAttack)
            {
                SwordAttack();
            }
        }
    }

    public void SwordAttack()
    {
        isAttacking = true;
        canAttack = false;
        Animator anim = Sword.GetComponent<Animator>();
        //Debug.Log("Animation trigger");
        anim.SetTrigger("isAttackingAnimation");
        StartCoroutine(ResetAttackCooldown());
        Invoke("changeAttack", 0.3f);
    }
    void changeAttack()
    {
        isAttacking = !isAttacking;
    }
    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;

    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(0.3f);
    }

}
