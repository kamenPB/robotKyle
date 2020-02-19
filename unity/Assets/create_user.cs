using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class create_user : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        User test_user = new User("dummy", "surdummy", 20);
        DatabaseHandler.PostUser(test_user, "0", msg_success);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void msg_success()
    {
        print("Success!");
    }
}
