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
    public int wood = 0;
    public int stones = 0;

    public Text woodNumber;
    public Text stoneNumber;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)){
            inventory();
        }
        woodNumber.text = wood.ToString();
        stoneNumber.text = stones.ToString();
    }

    public void addItem(string _item)
    {
        if(_item == "wood")
        {
            wood = wood + 3;
        }
        if(_item == "rock")
        {
            stones++;
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
