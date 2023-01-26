using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
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
                FireGun();
            }
        }
    }

    private void FireGun()
    {
        Vector3 correctRay = new Vector3(0f, -0f, 0f);
        Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        RaycastHit hitInfo;
        Debug.DrawRay(ray.origin+correctRay, ray.direction * 100, Color.red, 2f);
        /*
        if(Physics.Raycast(ray,out hitInfo, 100))
        {
            var health = hitInfo.collider.GetComponent<Health>();

            if(healh!= null)
            {
                health.TakeDamage(damage);
            }
        }*/

    }
}
