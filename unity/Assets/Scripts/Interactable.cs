using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Interactable : MonoBehaviour {
    [HideInInspector]
    public Hand m_ActiveHand = null;

    void OnDestroy() {
        if (m_ActiveHand != null) {
            m_ActiveHand.RemoveInteractable(gameObject.GetComponent<Interactable>());
            print("Object dead.");
        }
    }
}