using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class clockwatch : MonoBehaviour
{
    public float time;
    public Transform sun;
    public TMP_Text txt;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        time = sun.localEulerAngles.x;
        txt.text = "Time: " + (time >0 && time < 90 ? "day" : "night");
    }
}
