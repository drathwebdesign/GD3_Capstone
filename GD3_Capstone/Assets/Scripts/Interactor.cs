using UnityEngine;

public class Interactor : MonoBehaviour {
    [SerializeField] InventorySystem inventorySystem;
    [SerializeField] MannequinInventoryManager mannequinInventoryManager;  // Reference to the mannequin inventory manager
    [SerializeField] Transform itemContainer;
    [SerializeField] LayerMask whatIsInteractable;
    [SerializeField] float range = 2f;

    private GameObject objectInHands = null;

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit interactable, range, whatIsInteractable)) {
                objectInHands = interactable.transform.gameObject;

                // Check if the object is a mannequin part by its tag or component
                if (objectInHands.CompareTag("MannequinPart")) {
                    // Determine the part type and update the MannequinInventoryManager
                    string partName = objectInHands.name;  // Assume part names like "Arm", "Head", "Leg"
                    mannequinInventoryManager.CollectPart(partName);

                    // Optional: Hide or destroy the part once picked up
                    Destroy(objectInHands);
                } else {
                    // Normal item pickup and add to general inventory
                    objectInHands.transform.SetParent(itemContainer);
                    objectInHands.transform.localPosition = Vector3.zero;
                    objectInHands.transform.localRotation = Quaternion.identity;

                    if (objectInHands.transform.TryGetComponent(out Rigidbody rb))
                        rb.isKinematic = true;

                    // Add item to the main inventory system
                    inventorySystem.AddItem(objectInHands);
                }
            }
        }
    }
}
