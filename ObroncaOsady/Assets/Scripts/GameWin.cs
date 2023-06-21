using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWin : MonoBehaviour
{

    public Transform target;
    Animator m_Animator;
    public GameWInScreen GameWInScreen;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    private void Update()
    {
        // var pos = GameObject.Find(m_Animator).transform.position;
        Vector3 maxDistance = new Vector3(3f, 3f, 3f);

       // Debug.Log(transform.position);
       // Debug.Log(target.transform.position);
        if (Mathf.Abs(maxDistance.x) > Mathf.Abs(transform.position.x - target.transform.position.x) && maxDistance.y > Mathf.Abs(transform.position.y - target.transform.position.y))
        {

            GameWInScreen.Setup();

        }
    }
}
