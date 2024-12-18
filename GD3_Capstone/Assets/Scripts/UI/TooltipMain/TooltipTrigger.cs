using UnityEngine;

public class TooltipTrigger : MonoBehaviour {
    public TooltipInfo tooltipInfo;       // Reference to TooltipInfo ScriptableObject
    public string requiredPartName;       // Name of the part required to interact with this object

    public string GetTooltipText(bool hasRequiredPart) {
        return hasRequiredPart ? tooltipInfo.hasPartTooltip : tooltipInfo.missingPartTooltip;
    }
}