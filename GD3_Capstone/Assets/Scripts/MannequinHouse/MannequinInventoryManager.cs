using UnityEngine;
using UnityEngine.UI;

public class MannequinInventoryManager : MonoBehaviour {
    public GameObject mannequinHouseCanvas;  // Canvas object for UI
    public Image armImage;                   // Image for the mannequin arm
    public Image headImage;                  // Image for the mannequin head
    public Image legImage;                   // Image for the mannequin leg

    public GameObject armTick;               // Tick mark for arm when restored
    public GameObject headTick;              // Tick mark for head when restored
    public GameObject legTick;               // Tick mark for leg when restored

    public Transform playerTransform;

    [SerializeField] private GameObject keyObject;  // Key GameObject to activate once all mannequins are restored

    //narration for the puzzle

    [SerializeField] AudioClip collectArm;
    [SerializeField] AudioClip collectLeg;
    [SerializeField] AudioClip collectHead;
    [SerializeField] AudioClip comfort1;
    


    private bool armCollected = false;
    private bool headCollected = false;
    private bool legCollected = false;

    private bool armRestored = false;
    private bool headRestored = false;
    private bool legRestored = false;

    private int restoredMannequinsCount = 0; // Counter for restored mannequins

    void Start() {
        mannequinHouseCanvas.SetActive(false);  // Hide canvas initially
        ResetUI();  // Ensure UI starts with black-and-white images and hidden ticks

        if (keyObject != null) {
            keyObject.SetActive(false);  // Ensure the key is initially inactive
        }
    }

    

    public void EnterMannequinHouse() {
        mannequinHouseCanvas.SetActive(true);  // Show the canvas
        UpdateUI();                            // Update the UI to current part status
    }

    public void ExitMannequinHouse() {
        mannequinHouseCanvas.SetActive(false);  // Hide the canvas when exiting
    }

    public void CollectPart(string part) {
        // Marks the part as collected and updates the UI to show it's collected
        switch (part) {
            case "MannequinArm":
                armCollected = true;
                armImage.color = Color.white;  // Change color to white to indicate it's collected
                SoundFXManager.Instance.PlaySoundFXClip(1, collectArm, playerTransform, 1f);
                break;
            case "MannequinHead":
                headCollected = true;
                headImage.color = Color.white;  // Change color to white
                SoundFXManager.Instance.PlaySoundFXClip(1, collectHead, playerTransform, 1f);
                break;
            case "MannequinLeg":
                legCollected = true;
                legImage.color = Color.white;  // Change color to white
                SoundFXManager.Instance.PlaySoundFXClip(1, collectLeg, playerTransform, 1f);
                break;
        }

        UpdateUI();  // Update the UI immediately
    }

    public bool HasPart(string partName) {
        switch (partName) {
            case "MannequinArm":
                return armCollected;
            case "MannequinHead":
                return headCollected;
            case "MannequinLeg":
                return legCollected;
            default:
                return false;
        }
    }

    public void RestorePart(string part, GameObject mannequin) {
        // Activate the specific part on the mannequin
        string partName = part.Replace("Mannequin", "").ToLower();  // Convert "MannequinArm" to "arm", "MannequinLeg" to "leg", etc.
        Transform partTransform = mannequin.transform.Find(partName);

        if (partTransform != null) {
            partTransform.gameObject.SetActive(true);  // Set the missing part active
            Debug.Log($"{partName} part activated on {mannequin.name}.");
            if (restoredMannequinsCount < 3)
            {
                SoundFXManager.Instance.PlaySoundFXClip(1, comfort1, playerTransform, 1f);              
            }
        } else {
            Debug.LogWarning($"Part '{partName}' not found on mannequin '{mannequin.name}'. Please check hierarchy and naming.");
        }

        // Mark the part as restored and update UI ticks
        switch (part) {
            case "MannequinArm":
                armRestored = true;
                armTick.SetActive(true);  // Show tick when arm is restored
                break;
            case "MannequinHead":
                headRestored = true;
                headTick.SetActive(true);  // Show tick when head is restored
                break;
            case "MannequinLeg":
                legRestored = true;
                legTick.SetActive(true);  // Show tick when leg is restored
                break;
        }

        // Change the mannequin's layer to Default
        mannequin.layer = LayerMask.NameToLayer("Default");
        restoredMannequinsCount++;
        Debug.Log($"{part} restored on {mannequin.name}. Total restored mannequins: {restoredMannequinsCount}");

        // Check if all mannequins are restored and activate the key if so
        if (restoredMannequinsCount >= 3) {
            ActivateKey();
        }
    }


    private void ActivateKey() {
        if (keyObject != null) {
            keyObject.SetActive(true);  // Activate the Key object
            Debug.Log("All mannequins restored! Key has been activated.");
        } else {
            Debug.LogWarning("Key object reference is missing. Please assign it in the Inspector.");
        }
    }

    private void UpdateUI() {
        // Update tick visibility based on restored status
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
