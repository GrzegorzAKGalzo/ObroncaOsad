using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Play : MonoBehaviour
{

    public void startGame()
    {
        SceneManager.LoadScene("Level1");
    }
    public void exitGame()
    {
        Application.Quit();
    }
}
