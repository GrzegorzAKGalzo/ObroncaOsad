using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class playerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groudnMask;
    public float jumpHeight = 3f;
    public float sprint = 1f;
    public Canvas backpack;
    public Canvas builidng;
    public mouseLook kamera;
     Vector3 velocity;
    private bool inventoryOpend = false;
    private bool buildingOpend = false;
    bool isGrouded;
    public AudioSource backpackSource;

    public int wood = 0;
    public int stones = 0;

    public TMP_Text woodNumber;
    public TMP_Text stoneNumber;
    // Update is called once per frame
    void Update()
    {
        isGrouded = Physics.CheckSphere(groundCheck.position, groundDistance, groudnMask);
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            sprint = 1.9f;
        } else
        {
            sprint = 1f;
        }
        if (isGrouded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {

            inventory();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {

            building();
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime * sprint);
        if(Input.GetButtonDown("Jump") && isGrouded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        velocity.y += gravity * 3 * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);



        woodNumber.text = wood.ToString();
        stoneNumber.text = stones.ToString();
    }


    void inventory()
    {
        backpack.enabled = !backpack.enabled;
        inventoryOpend = !inventoryOpend;
        Cursor.visible = inventoryOpend;
        kamera.enabled = !inventoryOpend;
        Cursor.lockState = CursorLockMode.Confined;
        backpackSource.Play();
    }
    public void building()
    {
        builidng.enabled = !builidng.enabled;
        buildingOpend = !buildingOpend;
        Cursor.visible = buildingOpend;
        kamera.enabled = !buildingOpend;
        Cursor.lockState = CursorLockMode.Confined;
        backpackSource.Play();
    }
    public void addItem(string _item)
    {
        if (_item == "wood")
        {
            wood = wood + 3;
        }
        if (_item == "rock")
        {
            stones++;
        }
    }
}
