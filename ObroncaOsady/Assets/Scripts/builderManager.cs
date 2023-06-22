using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class builderManager : MonoBehaviour
{
    bool holdingObject = false;
    RaycastHit hit;
    float distance = 100f;
    Vector3 targetLocation;
    Vector3 targetNormal;
    float Rotation = 0f;
    GameObject prefab;
    GameObject tempGun;
    [SerializeField]
    public Material good;
    [SerializeField]
    public Material bad;
    Material org;
    public void setObject(GameObject choice)
    {
        prefab = choice;
        this.GetComponent<playerMovement>().building();
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, Camera.main.transform.forward * distance,Color.green);

        if (Input.GetKeyDown("f"))
        {

            tempGun = (GameObject)Instantiate(prefab, targetLocation, prefab.transform.rotation);
            //Instantiate(prefab, targetLocation, Quaternion.Euler(0, 0, 180f));
            org = tempGun.GetComponent<MeshRenderer>().material;
            //tempGun.transform.Rotate(new Vector3(90f, 0, 0));
            tempGun.GetComponent<MeshCollider>().enabled = false;
            Debug.Log("Open Menu");
            holdingObject = true;

        }



        if (Physics.Raycast(ray.origin, Camera.main.transform.forward, out hit, distance))
        {
            targetLocation = hit.point;
            targetNormal = hit.normal;
        }
        



        if (holdingObject)
        {
            tempGun.GetComponent<MeshRenderer>().material = bad;
            tempGun.transform.position = targetLocation;
            tempGun.transform.rotation = Quaternion.FromToRotation(Vector3.up, targetNormal);
            tempGun.transform.Rotate(new Vector3(0f, Rotation, 0f));
            if (Vector3.Distance(tempGun.transform.position, transform.position ) < 10)
            {
                tempGun.GetComponent<MeshRenderer>().material = good;
            }
                
            if (Input.GetKeyDown(KeyCode.Mouse0) && Vector3.Distance(tempGun.transform.position, transform.position) < 10)
            {
                holdingObject = false;
                tempGun.GetComponent<MeshCollider>().enabled = true;
                tempGun.GetComponent<MeshRenderer>().material = org;
            }
            if (Input.GetKey("r"))
            {
                Rotation++;
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                holdingObject = false;
                Destroy(tempGun);
            }
        }
    }
}
