using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeChop : MonoBehaviour
{
    public GameObject drop;
    public int hitPoints = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hitPoints <= 0)
        {
            transform.Find("topPart").GetComponent<Rigidbody>().useGravity = true;
            transform.Find("topPart").GetComponent<Rigidbody>().AddForce(Vector3.forward);
            Destroy(transform.Find("topPart").gameObject, 7f);
            Destroy(this);
        }
    }

    public void takeDamge()
    {
        hitPoints--;
    }


}
