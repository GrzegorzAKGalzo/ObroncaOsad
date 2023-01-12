using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAttack : MonoBehaviour
{

    private bool isAttacking;
    private Transform target;
    Animator m_Animator;
    // Start is called before the first frame update
    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update2()
    {
       // var pos = GameObject.Find(m_Animator).transform.position;
        Vector3 maxDistance = new Vector3(10.0f, 10.0f, 10.0f);
        if (maxDistance.x < m_Animator.transform.position.x - target.transform.position.x && maxDistance.y < m_Animator.transform.position.y - target.transform.position.y)
        {
            m_Animator.SetBool("IsAttacking", true);
            
        }
    }
}
