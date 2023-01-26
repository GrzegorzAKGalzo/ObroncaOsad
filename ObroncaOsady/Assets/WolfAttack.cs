using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAttack : MonoBehaviour
{

    private bool isAttacking;
    public Transform target;
    Animator m_Animator;
    public TakingDamage player;
    // Start is called before the first frame update
    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
       // var pos = GameObject.Find(m_Animator).transform.position;
        Vector3 maxDistance = new Vector3(1.7f, 1.7f, 1.7f);
    
       // Debug.Log(transform.position);
       // Debug.Log(target.transform.position);
        if (Mathf.Abs(maxDistance.x) > Mathf.Abs(transform.position.x - target.transform.position.x) && maxDistance.y > Mathf.Abs(transform.position.y - target.transform.position.y))
        {
            Debug.Log("Test");
            m_Animator.SetBool("IsAttacking", true);
            player.PlayerTakeDamage(100);


        }
    }
}
