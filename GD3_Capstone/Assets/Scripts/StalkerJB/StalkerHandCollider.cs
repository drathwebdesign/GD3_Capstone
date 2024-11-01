using UnityEngine;

public class StalkerHandCollider : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Stalker's hand hit the player!");
        }
    }
}