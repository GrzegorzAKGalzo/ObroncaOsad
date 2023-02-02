using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class backpackManagment : MonoBehaviour
{
    private Button[] slot = new Button[12];
    public AudioSource backpackSource;
    bool inventoryOpend = false;
    public Canvas backpack;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)){
            inventory();
        }
    }
    void inventory()
    {

        inventoryOpend = !inventoryOpend;
        Cursor.visible = inventoryOpend;
        backpack.enabled = !backpack.enabled;
        Cursor.lockState = CursorLockMode.Confined;
        backpackSource.Play();
    }
}
