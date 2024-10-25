using UnityEngine;
using TMPro;

public class TooltipDisplay : MonoBehaviour {
    public float interactionRange = 5f;               // Distance within which the player can interact
    public LayerMask interactableLayer;               // Layer for all interactive objects
    public TextMeshProUGUI tooltipUI;                 // Reference to TextMeshProUGUI for tooltip text
    private TooltipInfo currentTooltip;               // Stores the current tooltip information

    void Update() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionRange, interactableLayer)) {
            // Check if the hit object has a TooltipTrigger component
            TooltipTrigger tooltipTrigger = hit.collider.GetComponent<TooltipTrigger>();
            if (tooltipTrigger != null) {
                ShowTooltip(tooltipTrigger.tooltipInfo);
            }
        } else {
            HideTooltip(); // Hide tooltip when nothing is in range
        }
    }

    void ShowTooltip(TooltipInfo tooltipInfo) {
        if (tooltipInfo != currentTooltip) {
            currentTooltip = tooltipInfo;
            tooltipUI.text = tooltipInfo.tooltipText;    // Set the tooltip text
            tooltipUI.gameObject.SetActive(true);        // Show the tooltip UI
        }
    }

    void HideTooltip() {
        if (currentTooltip != null) {
            currentTooltip = null;
            tooltipUI.gameObject.SetActive(false);       // Hide the tooltip UI
        }
    }
}