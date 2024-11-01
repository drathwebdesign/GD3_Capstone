using UnityEngine;

public class EndGameStalkerHandCollider : MonoBehaviour{
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Stalker's hand hit the player!");

            // Activate the final scene via GameManager
            if (GameManager.instance != null) {
                GameManager.instance.ActivateFinalScene();
            } else {
                Debug.LogWarning("GameManager instance not found!");
            }
        }
    }
}