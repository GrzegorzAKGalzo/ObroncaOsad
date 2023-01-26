using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    public float moveSpeed = 20f;
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    bool isShooting;
    bool isRunning;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var movment = new Vector3(horizontal, 0f, vertical).normalized;


        transform.Rotate(Vector3.up, horizontal * turnSpeed * Time.deltaTime);


        if (vertical != 0)
        {
            if (vertical > 0)
            {
                isRunning = true;
                isShooting = false;
                m_Animator.SetBool("IsShooting", isShooting);
                m_Animator.SetBool("IsRunning", isRunning);
                m_Rigidbody.MovePosition(m_Rigidbody.position + transform.forward * moveSpeed * vertical);
            }
            else
            {
                isRunning = false;
                m_Animator.SetBool("IsRunning", isRunning);
                m_Rigidbody.MovePosition(m_Rigidbody.position + transform.forward * moveSpeed * vertical/2);
            }

           
        }

        else
        {
            isRunning = false;
            m_Animator.SetBool("IsRunning", isRunning);
        }

        if (Input.GetKey("f") && vertical==0)
        {
            isShooting = true;
            m_Animator.SetBool("IsShooting", isShooting);
        }
        else
        {
            isShooting =false;
            m_Animator.SetBool("IsShooting", isShooting);
        }

    }
}
