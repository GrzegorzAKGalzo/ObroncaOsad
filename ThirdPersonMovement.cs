using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
   

    private float turnSmoothVelocity;



// Update is called once per frame
void Update()
{
    float moveLR = Input.GetAxisRaw("Horizontal");
    float moveFB = Input.GetAxisRaw("Vertical");
    Vector3 direction = new Vector3(moveLR, 0f, moveFB).normalized;
    if (direction.magnitude > 0.1f)
    {
        float targerAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targerAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        Vector3 moveDir = Quaternion.Euler(0f, targerAngle, 0f) * Vector3.forward;
        controller.Move(moveDir * speed * Time.deltaTime);
    }
}

}


