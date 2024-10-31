using UnityEngine;
using System.Collections;

public class Interactor : MonoBehaviour {
    [SerializeField] private InventorySystem inventorySystem;  // Reference to the main inventory system
    [SerializeField] private MannequinInventoryManager mannequinInventoryManager;  // Reference to the mannequin inventory manager
    [SerializeField] private TooltipDisplay tooltipDisplay;  // Reference to the tooltip display system
    [SerializeField] private LayerMask whatIsInteractable;
    [SerializeField] private float range = 2f;
    public Animator diggingAnimation;
    private AudioSource audioSource;
    public AudioClip DiggingSound;


    private GameObject objectInHands = null;

    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit interactable, range, whatIsInteractable)) {
                objectInHands = interactable.transform.gameObject;

                // Handle different interactions based on the tag of the interactable object
                if (objectInHands.CompareTag("MannequinPart")) {
                    CollectMannequinPart(objectInHands);   // Collect a mannequin part
                } else if (objectInHands.CompareTag("MannequinBroken")) {
                    HandleMannequinInteraction(objectInHands);  // Mannequin interaction
                } else if (objectInHands.CompareTag("Door")) {
                    HandleDoorInteraction(objectInHands);  // Door interaction
                } else if (objectInHands.CompareTag("Item")) {
                    AddItemToInventory(objectInHands);   // Pick up a regular item
                } else if (objectInHands.CompareTag("GraveStone")) {
                    HandleGraveStoneInteraction(objectInHands);  // GraveStone interaction function
                }
            }
        }
    }

    private void HandleGraveStoneInteraction(GameObject graveStone) {
        // Check if the player is holding the Shovel
        bool hasShovel = inventorySystem.currentHeldObject != null &&
                         inventorySystem.currentHeldObject.name == "Shovel";

        TargetObjectActivator activator = graveStone.GetComponent<TargetObjectActivator>();

        // Proceed only if the player has the shovel and the interaction has not already occurred
        if (hasShovel && activator != null && !activator.hasBeenActivated) {
            // Activate the specified object and mark it as interacted
            GameObject targetObject = activator.targetObject;
            if (targetObject != null) {
                targetObject.SetActive(true);  // Activate the target object
                activator.hasBeenActivated = true;  // Mark the interaction as completed
                audioSource.PlayOneShot(DiggingSound);
                PlayDiggingAnimation();
                Debug.Log("GraveStone interaction successful! The target object has been activated.");
            } else {
                Debug.LogWarning("No target object is set for this GraveStone.");
            }
        } else if (!hasShovel) {
            Debug.Log("You need the Shovel in hand to interact with the GraveStone.");
        } else if (activator != null && activator.hasBeenActivated) {
            Debug.Log("This GraveStone has already been dug up.");
        }
    }

    private void HandleMannequinInteraction(GameObject mannequin) {
        TooltipTriggerMannequin tooltipTrigger = mannequin.GetComponent<TooltipTriggerMannequin>();

        if (tooltipTrigger != null) {
            string requiredPartName = tooltipTrigger.requiredPartName;
            bool hasRequiredPart = mannequinInventoryManager.HasPart(requiredPartName);

            // Show the correct tooltip for mannequins
            tooltipDisplay.ShowTooltipMannequin(tooltipTrigger.tooltipInfo, hasRequiredPart);

            if (hasRequiredPart) {
                // If the required part is in the mannequin inventory, restore the part on the mannequin
                RestoreMannequinPart(mannequin);
            } else {
                Debug.Log("You need the " + requiredPartName + " to complete this mannequin.");
            }
        }
    }

    private void HandleDoorInteraction(GameObject door) {
        TooltipTrigger tooltipTrigger = door.GetComponent<TooltipTrigger>();

        if (tooltipTrigger != null) {
            string requiredKeyName = tooltipTrigger.requiredPartName;
            bool hasRequiredKey = inventorySystem.currentHeldObject != null &&
                                  inventorySystem.currentHeldObject.name == requiredKeyName;

            // Show the correct tooltip for doors
            tooltipDisplay.ShowTooltip(tooltipTrigger.tooltipInfo, hasRequiredKey);

            if (hasRequiredKey || string.IsNullOrEmpty(requiredKeyName)) {
                // Open the door if the player has the required key or no key is required
                OpenDoor(door);
            } else {
                Debug.Log("You need the " + requiredKeyName + " to open this door.");
            }
        } else {
            // Open doors that don’t have a key requirement or TooltipTrigger
            OpenDoor(door);
        }
    }

    private void OpenDoor(GameObject door) {
        Animator[] childAnimators = door.GetComponentsInChildren<Animator>();
        Animator singleAnimator = door.GetComponent<Animator>();

        if (childAnimators.Length > 1) {
            foreach (Animator animator in childAnimators) {
                animator.SetTrigger("Open");
            }
            StartCoroutine(SetLayerToDefaultAfterAnimation(door));
        } else if (singleAnimator != null) {
            singleAnimator.SetTrigger("Open");
            StartCoroutine(SetLayerToDefaultAfterAnimation(door));
        } else {
            Debug.LogWarning("The door does not have an Animator component.");
        }
    }

    private IEnumerator SetLayerToDefaultAfterAnimation(GameObject door) {
        yield return new WaitForSeconds(0.5f); // Adjust based on animation length
        door.layer = LayerMask.NameToLayer("Default");
    }

    private void CollectMannequinPart(GameObject part) {
        string partName = part.name;  // Expect names like "MannequinArm", "MannequinHead", "MannequinLeg"
        mannequinInventoryManager.CollectPart(partName);

        // Remove the part from the scene after collecting
        Destroy(part);
    }

    private void RestoreMannequinPart(GameObject mannequin) {
        if (mannequinInventoryManager == null) {
            Debug.LogError("MannequinInventoryManager is not assigned in Interactor script.");
            return;
        }

        string requiredPart = GetRequiredPartForMannequin(mannequin.name);
        if (string.IsNullOrEmpty(requiredPart)) {
            Debug.LogWarning("Unknown mannequin type or no required part specified.");
            return;
        }

        bool hasRequiredPart = mannequinInventoryManager.HasPart(requiredPart);
        if (hasRequiredPart) {
            Debug.Log($"Restoring {requiredPart} to {mannequin.name}.");
            mannequinInventoryManager.RestorePart(requiredPart, mannequin);
        } else {
            Debug.Log("Player does not have the required part: " + requiredPart);
        }
    }

    private void AddItemToInventory(GameObject item) {
        inventorySystem.AddItem(item);  // Add item to the player's inventory
    }

    private string GetRequiredPartForMannequin(string mannequinName) {
        switch (mannequinName) {
            case "MannequinMLF":
                return "MannequinArm";
            case "MannequinMLL":
                return "MannequinLeg";
            case "MannequinMH":
                return "MannequinHead";
            default:
                return null;
        }
    }

    public void PlayDiggingAnimation()
    {
        { 
            diggingAnimation.enabled = true;
            Debug.Log("Playing digging animation");

        }
    }
}