using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class clockwatch : MonoBehaviour
{
    public float time;
    public static int day;
    string timeofDay;
    public Transform sun;
    public TMP_Text txt;
    public WaveSpawner spawner;
    // Start is called before the first frame update
    void Start()
    {
        day = -2;
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0 && time < 90)
        {
            timeofDay = "day";
        } else
        {
            timeofDay = "night";
        }
        if(sun.localEulerAngles.x == 0 || Input.GetKeyDown("p"))
        {
            day++;
            for(int i = 0; i < day; i++)
            {
                spawner.enemiesToSpawn.Add(GameObject.Find("Wolf"));
            }
            if (day > 2)
            {
                GameObject.Find("Wolf").GetComponent<WolfNewMovement>().health += 30;
                GameObject.Find("Wolf").GetComponent<WolfNewMovement>().attackSpeed += 1;
                GameObject.Find("Wolf").GetComponent<WolfNewMovement>().sightRange += 3;
            }
        }
        time = sun.localEulerAngles.x;
        txt.text = "Time: " + timeofDay + " Day: "+day.ToString();
    }
}
