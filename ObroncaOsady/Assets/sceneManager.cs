using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class sceneManager : MonoBehaviour
{
    public TMP_Text da;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        da.text = clockwatch.day.ToString() + "DAYS";
        if (Input.GetKeyDown("space"))
        {
            loadGame();
        }
    }
    public void loadGame()
    {
        SceneManager.LoadScene("Level1");
    }
}
