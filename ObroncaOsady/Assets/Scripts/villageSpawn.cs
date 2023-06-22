using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class villageSpawn : MonoBehaviour
{


    public Vector3 playerPosition;
    public Terrain terrain;
    public List<GameObject> house;
    public GameObject campFire;
    public float radius = 80f;
    public float radian = 0 * Mathf.PI / 180;
    Vector3 offset = new Vector3(1f, 0f, 1f);
    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.Find("Player").transform.position;
        Instantiate(campFire, Vector3.Scale(offset, playerPosition), Quaternion.identity);
        for (int i =1; i <10; i++)
        {
            offset = new Vector3(radius * Mathf.Cos(radian) + playerPosition.x , terrain.SampleHeight(playerPosition + offset), radius * Mathf.Sin(radian) + playerPosition.z);
            Instantiate(house[Random.Range((int)0, (int)house.Count)], offset, Quaternion.identity);
            radian = i * 360/10 * Mathf.PI / 180;
        }
    }

}
