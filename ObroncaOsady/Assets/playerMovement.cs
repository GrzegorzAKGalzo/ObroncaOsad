using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groudnMask;
    public float jumpHeight = 3f;
     Vector3 velocity;
    bool isGrouded; 
    // Update is called once per frame
    void Update()
    {
        isGrouded = Physics.CheckSphere(groundCheck.position, groundDistance, groudnMask);

        if(isGrouded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
        if(Input.GetButtonDown("Jump") && isGrouded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * 3 * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
}
