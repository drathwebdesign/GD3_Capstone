using UnityEngine;
using TMPro;

public class TooltipDisplay : MonoBehaviour {
    public float interactionRange = 5f;               // Distance within which the player can interact
    public LayerMask interactableLayer;               // Layer for all interactive objects
    public TextMeshProUGUI tooltipUI;                 // Reference to TextMeshProUGUI for tooltip text
    private TooltipInfo currentTooltip;               // Stores the current tooltip information

    [SerializeField] private MannequinInventoryManager mannequinInventoryManager;  // Reference to MannequinInventoryManager

    void Update() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionRange, interactableLayer)) {
            TooltipTrigger tooltipTrigger = hit.collider.GetComponent<TooltipTrigger>();
            if (tooltipTrigger != null) {
                bool hasPart = mannequinInventoryManager.HasPart(tooltipTrigger.requiredPartName);
                Debug.Log($"Tooltip for {tooltipTrigger.requiredPartName}: hasPart = {hasPart}");  // Debug to check hasPart value
                ShowTooltip(tooltipTrigger.tooltipInfo, hasPart);
            }
        } else {
            HideTooltip(); // Hide tooltip when nothing is in range
        }
    }



    private void ShowTooltip(TooltipInfo tooltipInfo, bool hasPart) {
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
