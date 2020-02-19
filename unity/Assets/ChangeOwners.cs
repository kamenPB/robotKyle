using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOwners : Photon.MonoBehaviour
{

    // public Interactable toss;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     toss += Toss_InteractableObjectGrabbed;
    //     StartCoroutine(Pop());    
    // }

    // private void Toss_InteractableObjectGrabbed(object sender) 
    // {
    //     toss.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.player.ID);
    // }

    // IEnumerator Pop() {
    //     yield return new WaitForSeconds(10f);
    //     PhotonNetwork.Destroy(this.gameObject);
    // }
}
