using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropItems : MonoBehaviour
{
    public GameObject drop;
    private void OnDestroy()
    {
        Instantiate(drop, transform.position, Quaternion.identity);
    }
}
