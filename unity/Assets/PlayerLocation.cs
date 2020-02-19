using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private bool LocationUpdated = false;

    void Update() {
        if ((PhotonNetwork.player.ID != -1) && !LocationUpdated)
        {
            if (PhotonNetwork.player.ID == 2)
            {
                LocationUpdated = true;
                Vector3 newPos = gameObject.transform.position + new Vector3(-3, 0, 0);
                gameObject.transform.position = newPos;
            }
        }
    }
}
