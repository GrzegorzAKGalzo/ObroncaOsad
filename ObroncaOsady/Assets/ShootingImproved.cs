using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingImproved : MonoBehaviour
{

    public Vector3 collision = Vector3.zero;

    public float damage = 100;

    public float fireRate = 0.5f;

    public float timer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            if (Input.GetKey("f"))
            {
                timer = 0f;
                Shoot();
            }
        }

    
    }

    void Shoot()
    {

        RaycastHit hitinfo;
        Vector3 correctRay = new Vector3(0f, -0f, 0f);
        Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);

        if (Physics.Raycast(ray, out hitinfo, 100))
        {
            Debug.Log(hitinfo.transform.name);
            Target target = hitinfo.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
           
            

        }
    }
  
}
