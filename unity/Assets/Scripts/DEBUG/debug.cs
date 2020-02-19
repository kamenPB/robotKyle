using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debug : MonoBehaviour
{
    
    public bool showLocationChange = false;

    private Vector3 loc;

    // Start is called before the first frame update
    void Start()
    {
        loc = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position != loc)
        {
            if (showLocationChange)
            {
                print("OBJECT BEING MOVED!! ");
                print(gameObject.transform.position);
            }
            loc = gameObject.transform.position;
        }
    }
}
