using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class healthManager : MonoBehaviour
{

    [SerializeField]
    public TMP_Text healthText;
    [SerializeField]
    public float healthValue;
    // Start is called before the first frame update
    public void takeDamge(float value)
    {
        healthValue = healthValue - value;
    }

    void Start()
    {
        healthValue = 100;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = healthValue.ToString();
    }
}
