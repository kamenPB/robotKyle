using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hand : MonoBehaviour
{
    public SteamVR_Action_Boolean m_GrabAction = null;
    public SteamVR_Action_Single m_GrabAction_Single = null;
    

    private SteamVR_Behaviour_Pose m_Pose = null;
    private Animator m_Animator = null;

    private FixedJoint m_Joint = null;

    [SerializeField]
    private Interactable m_CurrentInteractable = null;
    [SerializeField]
    public HashSet<Interactable> m_ContactInteractables = new HashSet<Interactable>();


    private void Awake() {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();
        m_Animator = GetComponent<Animator>();

        m_GrabAction_Single[m_Pose.inputSource].onChange += Grab;
    }

    private void Update() {
        
        // Down
        if (m_GrabAction.GetStateDown(m_Pose.inputSource)) { 
            print(m_Pose.inputSource + " Trigger Down");

            //m_Animator.SetBool("Point", true);
            //m_Animator.SetFloat("GrabBlend", 1);
            Pickup();
        }

        // Up
        if (m_GrabAction.GetStateUp(m_Pose.inputSource)) {
            print(m_Pose.inputSource + " Trigger Up");
            Drop();
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Interactable")) {
            m_ContactInteractables.Add(other.gameObject.GetComponent<Interactable>());
        }      
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Interactable")) {
            m_ContactInteractables.Remove(other.gameObject.GetComponent<Interactable>());
        }
    }

    private void Grab(SteamVR_Action_Single action, SteamVR_Input_Sources source, float axis, float delta) {
        m_Animator.SetFloat("GrabBlend", axis);
    }

    public void Pickup() 
    {
        // Get nearest 
        m_CurrentInteractable = GetNearestInteractable();

        // Null check
        if (!m_CurrentInteractable) {
            return;
        }

        // Transfer the ownership to the current player
         if (m_CurrentInteractable.GetComponent<PhotonView>().ownerId != PhotonNetwork.player.ID)
        {
            m_CurrentInteractable.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.player.ID);
        }

        // Already held, check
        if (m_CurrentInteractable.m_ActiveHand) {
            m_CurrentInteractable.m_ActiveHand.Drop();  
        }

        // Position
        m_CurrentInteractable.transform.position = transform.position;

        // Attach
        Rigidbody targetBody = m_CurrentInteractable.GetComponent<Rigidbody>();
        m_Joint.connectedBody = targetBody;

        // Set active hand
        m_CurrentInteractable.m_ActiveHand = this;
    }

    public void Drop()
    {

        // Null check
        if (!m_CurrentInteractable) {
            return;
        }
        // Apply velocity
        Rigidbody targetBody = m_CurrentInteractable.GetComponent<Rigidbody>();
        targetBody.velocity = m_Pose.GetVelocity();
        targetBody.angularVelocity = m_Pose.GetAngularVelocity();

        // Detach
        m_Joint.connectedBody = null;

        // Clear
        m_CurrentInteractable.m_ActiveHand = null;
        m_CurrentInteractable = null;

    }

    public void RemoveInteractable(Interactable script) {
        if(m_CurrentInteractable == script) {
            m_CurrentInteractable = null;
        }
        m_ContactInteractables.Remove(script);
    }


    private Interactable GetNearestInteractable() {
        Interactable nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;

        foreach (Interactable interactable in m_ContactInteractables) {
            distance = (interactable.transform.position - transform.position).sqrMagnitude;

            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = interactable;
            }
        }

        return nearest;
    }
}
