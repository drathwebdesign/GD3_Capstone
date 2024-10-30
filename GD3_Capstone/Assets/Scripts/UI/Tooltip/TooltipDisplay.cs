using UnityEngine;
using TMPro;

public class TooltipDisplay : MonoBehaviour {
    public float interactionRange = 5f;               // Distance within which the player can interact
    public LayerMask interactableLayer;               // Layer for all interactive objects
    public TextMeshProUGUI tooltipUI;                 // Reference to TextMeshProUGUI for tooltip text

    private TooltipInfo currentTooltip;               // Stores the current tooltip information
    [SerializeField] private InventorySystem inventorySystem;  // Reference to InventorySystem

    void Update() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionRange, interactableLayer)) {
            TooltipTrigger tooltipTrigger = hit.collider.GetComponent<TooltipTrigger>();
            if (tooltipTrigger != null) {
                bool hasRequiredPart;

                // Check if the object is a mannequin or a door based on tag or other criteria
                if (hit.collider.CompareTag("MannequinBroken")) {
                    // Mannequin: Check if required part is anywhere in the inventory
                    hasRequiredPart = inventorySystem.inventory.Exists(item => item.name == tooltipTrigger.requiredPartName);
                } else if (hit.collider.CompareTag("Door")) {
                    // Door: Check only the currently held item in hand
                    hasRequiredPart = inventorySystem.currentHeldObject != null &&
                                      inventorySystem.currentHeldObject.name == tooltipTrigger.requiredPartName;
                } else {
                    hasRequiredPart = false;  // Default to false if neither type
                }

                ShowTooltip(tooltipTrigger.tooltipInfo, hasRequiredPart);
            }
        } else {
            HideTooltip(); // Hide tooltip when nothing is in range
        }
    }

    public void ShowTooltip(TooltipInfo tooltipInfo, bool hasPart) {
        if (tooltipInfo != currentTooltip) {
            currentTooltip = tooltipInfo;
            tooltipUI.text = tooltipInfo.GetTooltip(hasPart);   // Fetch the correct tooltip text
            tooltipUI.gameObject.SetActive(true);               // Show the tooltip UI
        }
    }

    private void HideTooltip() {
        if (currentTooltip != null) {
            currentTooltip = null;
            tooltipUI.gameObject.SetActive(false);              // Hide the tooltip UI
        }
    }
}
