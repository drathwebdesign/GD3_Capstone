using UnityEngine;
using System.Collections;

public class Interactor : MonoBehaviour {
    [SerializeField] InventorySystem inventorySystem;  // Reference to the main inventory system
    [SerializeField] MannequinInventoryManager mannequinInventoryManager;  // Reference to the mannequin inventory manager
    [SerializeField] Transform itemContainer;
    [SerializeField] LayerMask whatIsInteractable;
    [SerializeField] float range = 2f;

    [SerializeField] TooltipInfo missingPartTooltip;  // Tooltip when player does not have the part
    [SerializeField] TooltipInfo hasPartTooltip;      // Tooltip when player has the part

    private GameObject objectInHands = null;

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit interactable, range, whatIsInteractable)) {
                objectInHands = interactable.transform.gameObject;

                if (objectInHands.CompareTag("MannequinPart")) {
                    // Handle collection of a mannequin part
                    CollectMannequinPart(objectInHands);
                } else if (objectInHands.CompareTag("MannequinBroken")) {
                    // Handle restoration of a mannequin part to the broken mannequin
                    RestoreMannequinPart(objectInHands);
                } else if (objectInHands.CompareTag("Door")) {
                    // Handle door opening
                    OpenDoor(objectInHands);
                } else if (objectInHands.CompareTag("Item")) {   // New case for adding items to inventory
                    AddItemToInventory(objectInHands);
                }
            }
        }
    }

    private void OpenDoor(GameObject door) {
        // Check if the door object has child animators (for double doors) or its own animator (for single doors)
        Animator[] childAnimators = door.GetComponentsInChildren<Animator>();
        Animator singleAnimator = door.GetComponent<Animator>();

        if (childAnimators.Length > 1) {
            // This is a double door setup with child animators
            foreach (Animator animator in childAnimators) {
                animator.SetTrigger("Open");
            }
            // Set the layer of the parent door to Default after opening
            StartCoroutine(SetLayerToDefaultAfterAnimation(door));
        } else if (singleAnimator != null) {
            // This is a single door setup with its own animator
            singleAnimator.SetTrigger("Open");
            // Set the layer of the single door to Default after opening
            StartCoroutine(SetLayerToDefaultAfterAnimation(door));
        } else {
            Debug.LogWarning("The door does not have an Animator component.");
        }
    }

    // Coroutine to wait for the animation to start and then set the layer to Default
    private IEnumerator SetLayerToDefaultAfterAnimation(GameObject door) {
        // Wait for the animation duration or a short delay (adjust as needed)
        yield return new WaitForSeconds(0.5f); // Change this value based on the animation length

        // Set the door or door parent layer to Default (layer 0) so it is no longer interactable
        door.layer = LayerMask.NameToLayer("Default");
    }



    private void AddItemToInventory(GameObject item) {
        inventorySystem.AddItem(item);  // Add item to InventorySystem
    }

    #region Mannequin
    private void CollectMannequinPart(GameObject part) {
        // Determine the part type and update the MannequinInventoryManager
        string partName = part.name;  // Expect names like "MannequinArm", "MannequinHead", "MannequinLeg"
        mannequinInventoryManager.CollectPart(partName);

        // Destroy the part in the scene since it has been collected
        Destroy(part);
    }

    private void RestoreMannequinPart(GameObject mannequin) {
        if (mannequinInventoryManager == null) {
            Debug.LogError("MannequinInventoryManager is not assigned in Interactor script.");
            return;
        }

        string requiredPart = GetRequiredPartForMannequin(mannequin.name);
        if (string.IsNullOrEmpty(requiredPart)) {
            Debug.LogWarning("Unknown mannequin type or no required part specified.");
            return;
        }

        // Check if the player has the required part in the inventory
        bool hasRequiredPart = mannequinInventoryManager.HasPart(requiredPart);
        if (hasRequiredPart) {
            Debug.Log($"Attempting to restore {requiredPart} on {mannequin.name}.");

            // Call RestorePart in MannequinInventoryManager, passing the part and mannequin GameObject
            mannequinInventoryManager.RestorePart(requiredPart, mannequin);
        } else {
            Debug.Log("Player does not have the required part: " + requiredPart);
        }
    }

    private string GetRequiredPartForMannequin(string mannequinName) {
        switch (mannequinName) {
            case "MannequinMLF":
                return "MannequinArm";
            case "MannequinMLL":
                return "MannequinLeg";
            case "MannequinMH":
                return "MannequinHead";
            default:
                return null;
        }
    }
    #endregion


}