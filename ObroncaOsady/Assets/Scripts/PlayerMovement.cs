using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    bool isShooting;

    public Transform cam;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;


    private float turnSmoothVelocity;



    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("f"))
        {
            isShooting = true;
            m_Animator.SetBool("IsShooting", isShooting);
        }
        else
        {
            isShooting = false;
            m_Animator.SetBool("IsShooting", isShooting);
            float moveLR = Input.GetAxis("Horizontal");
            float moveFB = Input.GetAxis("Vertical");
            Vector3 direction = new Vector3(moveLR, 0f, moveFB).normalized;
            if (direction.magnitude > 0.1f)
            {
                float targerAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targerAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                Vector3 moveDir = Quaternion.Euler(0f, targerAngle, 0f) * Vector3.forward;
                m_Rigidbody.MovePosition(m_Rigidbody.position+moveDir * speed * Time.deltaTime);
            }
            /*
            m_Movement.Set(horizontal, 0f, vertical);
            m_Movement.Normalize();
            bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
            bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
            bool isRunning = hasHorizontalInput || hasVerticalInput;
            m_Animator.SetBool("IsRunning", isRunning);
            Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
            m_Rotation = Quaternion.LookRotation(desiredForward);*/
        }

    }

  
}
