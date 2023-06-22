using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class healthManager : MonoBehaviour
{

    [SerializeField]
    public TMP_Text healthText;
    [SerializeField]
    public float healthValue;
    [SerializeField]
    public AudioSource damgeSound;
    // Start is called before the first frame update
    public void takeDamge(float value)
    {
        healthValue = healthValue - value;
        GetComponentInChildren<CameraShake>().enabled = true;
        damgeSound.Play();
    }

    void Start()
    {
        healthValue = 100;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = healthValue.ToString();
        if (healthValue == 0)
        {
            SceneManager.LoadScene("DeathScene");
        }
    }
}
