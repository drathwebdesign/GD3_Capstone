using UnityEngine;

[CreateAssetMenu(fileName = "NewTooltip", menuName = "Tooltip/TooltipInfo")]
public class TooltipInfo : ScriptableObject {
    [TextArea(3, 5)]
    public string tooltipText;
}