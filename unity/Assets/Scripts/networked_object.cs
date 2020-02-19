using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class networked_object: Photon.MonoBehaviour
{
    // Start is called before the first frame update.

    //public GameObject myCube;

    private Vector3 obj_location_dest;

    private bool moving = false;
   

    [SerializeField]
    private float networkRefreshRate = 0.2f;
    
    private bool isGrabbable = true;
    
 
    void Start() {
  
    }

    // Update is called once per frame
    void Update()
    {
        if (this.photonView.isMine) 
        {
            if (gameObject.GetComponent<Collider>().attachedRigidbody.useGravity == false)
            {
                gameObject.GetComponent<Collider>().attachedRigidbody.useGravity = true;
            }
            //object is mine.
            //if we are moving -> RPC Location.
            moving = true;
            //if currently moving object.
            
            //broadcast new object position after pre-determined time.

            if(gameObject.transform.position != obj_location_dest){
                obj_location_dest = gameObject.transform.position;
                photonView.RPC("Object_Position", PhotonTargets.Others, gameObject.transform.position, false, PhotonNetwork.ServerTimestamp);
            }                
        } else {
            //Not my object.
            if (isGrabbable == true) {
                //I was owner. Send final RPC so others can grab object.
                photonView.RPC("Object_Position", PhotonTargets.Others, gameObject.transform.position, true, PhotonNetwork.ServerTimestamp);
            } else {
                //Update project.
                Object_Update_Location();

            }

        }
        
     
    }

    private void Object_Update_Location(){
        if (gameObject.transform.position != obj_location_dest) gameObject.transform.position = obj_location_dest;

    }
[PunRPC]
    void Object_Position(Vector3 object_new_pos, bool object_is_grabbable, int time)
    {
        print("RPC Received");
        if (isGrabbable == false && object_is_grabbable == true){
            //someone else owns this object.
            //gravity is already off.

            //enable gravity.
            gameObject.GetComponent<Collider>().attachedRigidbody.useGravity = true;
            //enable grab.
            isGrabbable = true;
        
        } else if (isGrabbable == true && object_is_grabbable == false){
            //owner now has new owner.
            
            //disable grab.
            isGrabbable = false;
            //disable gravity.
            gameObject.GetComponent<Collider>().attachedRigidbody.useGravity = false;
        }  else {
            //Handles both the following cases:
            //if (isGrabbable == false && object_is_grabbable == false)
            //if (isGrabbable == true && object_is_grabbable == true)
            //keep all parameters the same.
        }

        //Update object location.
        obj_location_dest = object_new_pos;

        //int deltaTime = PhotonNetwork.ServerTimestamp - time;
    }
}