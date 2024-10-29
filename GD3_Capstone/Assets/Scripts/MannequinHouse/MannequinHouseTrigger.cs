using UnityEngine;

public class MannequinHouseTrigger : MonoBehaviour {
    public MannequinInventoryManager inventoryManager;  // Reference to the inventory manager

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            inventoryManager.EnterMannequinHouse();  // Show the mannequin house UI
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            inventoryManager.ExitMannequinHouse();  // Hide the mannequin house UI
        }
    }
}