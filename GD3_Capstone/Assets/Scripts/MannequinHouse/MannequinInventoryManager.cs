using UnityEngine;
using UnityEngine.UI;

public class MannequinInventoryManager : MonoBehaviour {
    public GameObject mannequinHouseCanvas;  // Canvas object
    public Image armImage;                   // Image for the mannequin arm
    public Image headImage;                  // Image for the mannequin head
    public Image legImage;                   // Image for the mannequin leg

    public GameObject armTick;               // Tick mark for arm when restored
    public GameObject headTick;              // Tick mark for head when restored
    public GameObject legTick;               // Tick mark for leg when restored

    private bool armCollected = false;
    private bool headCollected = false;
    private bool legCollected = false;

    private bool armRestored = false;
    private bool headRestored = false;
    private bool legRestored = false;

    void Start() {
        mannequinHouseCanvas.SetActive(false);  // Hide canvas initially
        ResetUI();  // Ensure UI starts with black-and-white images and hidden ticks
    }

    public void EnterMannequinHouse() {
        mannequinHouseCanvas.SetActive(true);  // Show the canvas
        UpdateUI();                            // Update the UI to current part status
    }

    public void ExitMannequinHouse() {
        mannequinHouseCanvas.SetActive(false);  // Hide the canvas when exiting
    }

    public void CollectPart(string part) {
        switch (part) {
            case "MannequinArm":  // Full name from hierarchy
                armCollected = true;
                armImage.color = Color.white;  // Change color to white to indicate it's collected
                break;
            case "MannequinHead":  // Full name from hierarchy
                headCollected = true;
                headImage.color = Color.white;  // Change color to white
                break;
            case "MannequinLeg":   // Full name from hierarchy
                legCollected = true;
                legImage.color = Color.white;  // Change color to white
                break;
        }

        UpdateUI();  // Update the UI immediately
    }

    public void RestorePart(string part) {
        switch (part) {
            case "MannequinArm":  // Full name from hierarchy
                armRestored = true;
                armTick.SetActive(true);  // Show tick when arm is restored
                break;
            case "MannequinHead":  // Full name from hierarchy
                headRestored = true;
                headTick.SetActive(true);  // Show tick when head is restored
                break;
            case "MannequinLeg":   // Full name from hierarchy
                legRestored = true;
                legTick.SetActive(true);  // Show tick when leg is restored
                break;
        }
    }

    private void UpdateUI() {
        // Check if each part has been collected and update tick visibility
        armTick.SetActive(armRestored);
        headTick.SetActive(headRestored);
        legTick.SetActive(legRestored);
    }

    private void ResetUI() {
        // Hide all ticks and start with blacked-out images
        armTick.SetActive(false);
        headTick.SetActive(false);
        legTick.SetActive(false);

        // Set initial colors to dark gray (black-and-white look)
        armImage.color = new Color(0.2f, 0.2f, 0.2f);  // Dark gray color for uncollected state
        headImage.color = new Color(0.2f, 0.2f, 0.2f);  // Dark gray color for uncollected state
        legImage.color = new Color(0.2f, 0.2f, 0.2f);   // Dark gray color for uncollected state
    }
}