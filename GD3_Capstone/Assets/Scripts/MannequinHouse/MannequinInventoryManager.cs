using UnityEngine;
using UnityEngine.UI;

public class MannequinInventoryManager : MonoBehaviour {
    public GameObject mannequinHouseUI;  // Reference to the UI Canvas
    public Image headImage;              // UI Image for the mannequin head
    public Image armImage;               // UI Image for the mannequin arm
    public Image legImage;               // UI Image for the mannequin leg

    public Sprite headColoredSprite;     // Colored sprite for head when picked up
    public Sprite armColoredSprite;      // Colored sprite for arm when picked up
    public Sprite legColoredSprite;      // Colored sprite for leg when picked up

    public GameObject headTick;          // Tick GameObject for head when restored
    public GameObject armTick;           // Tick GameObject for arm when restored
    public GameObject legTick;           // Tick GameObject for leg when restored

    private bool headPickedUp = false;
    private bool armPickedUp = false;
    private bool legPickedUp = false;

    private bool headRestored = false;
    private bool armRestored = false;
    private bool legRestored = false;

    void Start() {
        mannequinHouseUI.SetActive(false);  // UI is hidden by default
        ResetUI();                          // Ensure the UI is set to its initial state
    }

    public void EnterMannequinHouse() {
        mannequinHouseUI.SetActive(true);  // Show the inventory UI when entering the house
        UpdateUI();                        // Update the UI based on current part status
    }

    public void ExitMannequinHouse() {
        mannequinHouseUI.SetActive(false);  // Hide the UI when leaving the house
    }

    public void PickUpPart(string part) {
        switch (part) {
            case "Head":
                headPickedUp = true;
                break;
            case "Arm":
                armPickedUp = true;
                break;
            case "Leg":
                legPickedUp = true;
                break;
        }
        UpdateUI();
    }

    public void RestorePart(string part) {
        switch (part) {
            case "Head":
                headRestored = true;
                break;
            case "Arm":
                armRestored = true;
                break;
            case "Leg":
                legRestored = true;
                break;
        }
        UpdateUI();
    }

    private void UpdateUI() {
        // Update head image and tick
        if (headPickedUp)
            headImage.sprite = headColoredSprite;
        headTick.SetActive(headRestored);

        // Update arm image and tick
        if (armPickedUp)
            armImage.sprite = armColoredSprite;
        armTick.SetActive(armRestored);

        // Update leg image and tick
        if (legPickedUp)
            legImage.sprite = legColoredSprite;
        legTick.SetActive(legRestored);
    }

    private void ResetUI() {
        // Reset to outline images and hide ticks
        headTick.SetActive(false);
        armTick.SetActive(false);
        legTick.SetActive(false);
    }
}