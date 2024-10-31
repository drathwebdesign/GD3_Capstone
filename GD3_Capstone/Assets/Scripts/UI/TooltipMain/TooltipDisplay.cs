using UnityEngine;
using TMPro;

public class TooltipDisplay : MonoBehaviour {
    public float interactionRange = 5f;               // Distance within which the player can interact
    public LayerMask interactableLayer;               // Layer for all interactive objects
    public TextMeshProUGUI tooltipUI;                 // Reference to TextMeshProUGUI for tooltip text

    private TooltipInfo currentTooltip;               // Stores the current tooltip information
    private TooltipInfoMannequin currentTooltipMannequin; // Stores the current mannequin-specific tooltip information
    [SerializeField] private InventorySystem inventorySystem;  // Reference to InventorySystem
    [SerializeField] private MannequinInventoryManager mannequinInventoryManager;  // Reference to MannequinInventoryManager

    void Update() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionRange, interactableLayer)) {
            // Check object type and display appropriate tooltip
            if (hit.collider.CompareTag("MannequinBroken")) {
                HandleMannequinTooltip(hit.collider.GetComponent<TooltipTriggerMannequin>());
            } else if (hit.collider.CompareTag("Door")) {
                HandleDoorTooltip(hit.collider.GetComponent<TooltipTrigger>());
            } else if (hit.collider.CompareTag("Item") || hit.collider.CompareTag("MannequinPart")) {
                HandleItemOrPartTooltip(hit.collider.GetComponent<TooltipTrigger>());  // Use general TooltipTrigger for items
            } else if (hit.collider.CompareTag("GraveStone")) {
                HandleGraveStoneTooltip(hit.collider.GetComponent<TooltipTrigger>());
            }
        } else {
            HideTooltip(); // Hide tooltip when nothing is in range
        }
    }

    private void HandleMannequinTooltip(TooltipTriggerMannequin tooltipTrigger) {
        if (tooltipTrigger != null) {
            bool hasRequiredPart = mannequinInventoryManager.HasPart(tooltipTrigger.requiredPartName);
            ShowTooltipMannequin(tooltipTrigger.tooltipInfo, hasRequiredPart);
        }
    }

    private void HandleDoorTooltip(TooltipTrigger tooltipTrigger) {
        if (tooltipTrigger != null) {
            bool hasRequiredKey = inventorySystem.currentHeldObject != null &&
                                  inventorySystem.currentHeldObject.name == tooltipTrigger.requiredPartName;
            ShowTooltip(tooltipTrigger.tooltipInfo, hasRequiredKey);
        }
    }

    private void HandleGraveStoneTooltip(TooltipTrigger tooltipTrigger) {
        if (tooltipTrigger != null) {
            // Check if the player is holding the Shovel
            bool hasShovel = inventorySystem.currentHeldObject != null &&
                             inventorySystem.currentHeldObject.name == "Shovel";

            ShowTooltip(tooltipTrigger.tooltipInfo, hasShovel);
        }
    }

    private void HandleItemOrPartTooltip(TooltipTrigger tooltipTrigger) {
        if (tooltipTrigger != null) {
            // Show tooltip for regular items or mannequin parts
            ShowTooltip(tooltipTrigger.tooltipInfo, true); // Always show as "available" since items can be picked up
        }
    }

    public void ShowTooltip(TooltipInfo tooltipInfo, bool hasPart) {
        if (tooltipInfo != currentTooltip) {
            currentTooltip = tooltipInfo;
            tooltipUI.text = tooltipInfo.GetTooltip(hasPart);   // Fetch the correct tooltip text
            tooltipUI.gameObject.SetActive(true);               // Show the tooltip UI
        }
    }

    public void ShowTooltipMannequin(TooltipInfoMannequin tooltipInfoMannequin, bool hasPart) {
        if (tooltipInfoMannequin != currentTooltipMannequin) {
            currentTooltipMannequin = tooltipInfoMannequin;
            tooltipUI.text = tooltipInfoMannequin.GetTooltip(hasPart);   // Fetch the correct tooltip text for mannequins
            tooltipUI.gameObject.SetActive(true);                        // Show the tooltip UI
        }
    }

    private void HideTooltip() {
        currentTooltip = null;
        currentTooltipMannequin = null;
        tooltipUI.gameObject.SetActive(false);              // Hide the tooltip UI
    }
}