using UnityEngine;

[CreateAssetMenu(menuName = "Tooltip/TooltipInfo")]
public class TooltipInfo : ScriptableObject {
    public string missingPartTooltip;  // Tooltip when player does not have the part
    public string hasPartTooltip;      // Tooltip when player has the part

    public string GetTooltip(bool hasPart) {
        return hasPart ? hasPartTooltip : missingPartTooltip;
    }
}
