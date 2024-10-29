using UnityEngine;

[CreateAssetMenu(fileName = "NewTooltip", menuName = "Tooltip/TooltipInfo")]
public class TooltipInfo : ScriptableObject {
    [TextArea(3, 5)]
    public string missingPartTooltip;   // Tooltip to show when player does NOT have the part

    [TextArea(3, 5)]
    public string hasPartTooltip;       // Tooltip to show when player HAS the part

    // Method to get the correct tooltip text based on whether the player has the part
    public string GetTooltip(bool hasPart) {
        return hasPart ? hasPartTooltip : missingPartTooltip;
    }
}
