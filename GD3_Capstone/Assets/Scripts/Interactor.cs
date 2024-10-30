using UnityEngine;
using System.Collections;

public class Interactor : MonoBehaviour {
    [SerializeField] private InventorySystem inventorySystem;  // Reference to the main inventory system
    [SerializeField] private MannequinInventoryManager mannequinInventoryManager;  // Reference to the mannequin inventory manager
    [SerializeField] private TooltipDisplay tooltipDisplay;  // Reference to the tooltip display system
    [SerializeField] private Transform itemContainer;
    [SerializeField] private LayerMask whatIsInteractable;
    [SerializeField] private float range = 2f;

    private GameObject objectInHands = null;

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit interactable, range, whatIsInteractable)) {
                objectInHands = interactable.transform.gameObject;

                if (objectInHands.CompareTag("MannequinPart")) {
                    CollectMannequinPart(objectInHands);
                } else if (objectInHands.CompareTag("MannequinBroken")) {
                    HandleMannequinInteraction(objectInHands);  // Mannequin interaction function
                } else if (objectInHands.CompareTag("Door")) {
                    HandleDoorInteraction(objectInHands);  // Door interaction function
                } else if (objectInHands.CompareTag("Item")) {
                    AddItemToInventory(objectInHands);
                }
            }
        }
    }

    private void HandleMannequinInteraction(GameObject mannequin) {
        TooltipTrigger tooltipTrigger = mannequin.GetComponent<TooltipTrigger>();

        if (tooltipTrigger != null) {
            string requiredPartName = tooltipTrigger.requiredPartName;
            bool hasRequiredPart = mannequinInventoryManager.HasPart(requiredPartName);

            // Show the appropriate tooltip based on whether the player has the required part in inventory
            tooltipDisplay.ShowTooltip(tooltipTrigger.tooltipInfo, hasRequiredPart);

            if (hasRequiredPart) {
                // Player has the required part in inventory, so restore the mannequin part
                RestoreMannequinPart(mannequin);
            } else {
                Debug.Log("You need the " + requiredPartName + " to complete this mannequin.");
            }
        }
    }

    private void HandleDoorInteraction(GameObject door) {
        TooltipTrigger tooltipTrigger = door.GetComponent<TooltipTrigger>();

        if (tooltipTrigger != null) {
            string requiredPartName = tooltipTrigger.requiredPartName;
            bool hasRequiredKey = inventorySystem.currentHeldObject != null &&
                                  inventorySystem.currentHeldObject.name == requiredPartName;

            // Show the appropriate tooltip based on whether the player is holding the required key
            tooltipDisplay.ShowTooltip(tooltipTrigger.tooltipInfo, hasRequiredKey);

            if (hasRequiredKey || string.IsNullOrEmpty(requiredPartName)) {
                // Player has the required key or no key is required, so open the door
                OpenDoor(door);
            } else {
                Debug.Log("You need the " + requiredPartName + " to open this door.");
            }
        } else {
            // Open doors that don’t have a key requirement or TooltipTrigger
            OpenDoor(door);
        }
    }

    private void OpenDoor(GameObject door) {
        Animator[] childAnimators = door.GetComponentsInChildren<Animator>();
        Animator singleAnimator = door.GetComponent<Animator>();

        if (childAnimators.Length > 1) {
            foreach (Animator animator in childAnimators) {
                animator.SetTrigger("Open");
            }
            StartCoroutine(SetLayerToDefaultAfterAnimation(door));
        } else if (singleAnimator != null) {
            singleAnimator.SetTrigger("Open");
            StartCoroutine(SetLayerToDefaultAfterAnimation(door));
        } else {
            Debug.LogWarning("The door does not have an Animator component.");
        }
    }

    private IEnumerator SetLayerToDefaultAfterAnimation(GameObject door) {
        yield return new WaitForSeconds(0.5f); // Adjust based on animation length
        door.layer = LayerMask.NameToLayer("Default");
    }

    private void CollectMannequinPart(GameObject part) {
        string partName = part.name;  // Example: "MannequinArm", "MannequinHead", "MannequinLeg"
        mannequinInventoryManager.CollectPart(partName);

        // Remove the part from the scene after collecting
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
            Debug.Log($"Restoring {requiredPart} to {mannequin.name}.");
            mannequinInventoryManager.RestorePart(requiredPart, mannequin);
        } else {
            Debug.Log("Player does not have the required part: " + requiredPart);
        }
    }

    private void AddItemToInventory(GameObject item) {
        inventorySystem.AddItem(item);  // Add item to the player's inventory
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
}
