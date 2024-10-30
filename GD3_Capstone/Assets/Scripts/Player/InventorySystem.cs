using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour {
    public List<GameObject> inventory;
    public GameObject currentHeldObject;
    private int maxHotbarItems = 8;
    [SerializeField] Transform itemContainer;  // ItemContainer positioned in front of the player

    public List<Image> hotbarSlots;  // List of UI images for each hotbar slot
    public List<Image> hotbarOutlines;  // List of UI outline images for each hotbar slot

    private void Start() {
        inventory = new List<GameObject>();
        UpdateHotbarUI();
    }

    void Update() {
        for (int i = 0; i < maxHotbarItems; i++) {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i) && i < inventory.Count) {
                if (currentHeldObject == inventory[i]) {
                    PutItemAway();
                } else {
                    SetCurrentHeldObject(inventory[i]);
                }
            }
        }
    }

    public void AddItem(GameObject item) {
        if (inventory.Count < maxHotbarItems) {
            inventory.Add(item);
            item.SetActive(false);  // Hide the item initially
            UpdateHotbarUI();
        } else {
            Debug.Log("Inventory is full. Cannot add more items.");
        }
    }

    private void PutItemAway() {
        if (currentHeldObject) {
            currentHeldObject.SetActive(false);
            // Reset parent to remove from itemContainer
            currentHeldObject.transform.SetParent(null);
            currentHeldObject = null;
        }
    }

    public void SetCurrentHeldObject(GameObject newItem) {
        PutItemAway();  // Puts away any currently held item before setting a new one

        // Move item to itemContainer in front of player
        newItem.transform.SetParent(itemContainer);
        newItem.transform.localPosition = Vector3.zero;  // Center it in itemContainer
        newItem.transform.localRotation = Quaternion.identity;

        // Set item to kinematic and change layer to Default if needed
        Rigidbody rb = newItem.GetComponent<Rigidbody>();
        if (rb != null) {
            rb.isKinematic = true;  // Set Rigidbody to kinematic
        }

        newItem.layer = LayerMask.NameToLayer("Default");  // Change layer to Default

        // Disable TooltipTrigger and Item scripts
        TooltipTrigger tooltipTrigger = newItem.GetComponent<TooltipTrigger>();
        if (tooltipTrigger != null) {
            tooltipTrigger.enabled = false;
        }

        Item itemScript = newItem.GetComponent<Item>();
        if (itemScript != null) {
            itemScript.enabled = false;
        }

        // Disable Collider
        Collider collider = newItem.GetComponent<Collider>();
        if (collider != null) {
            collider.enabled = false;  // Turn off the collider
        }

        currentHeldObject = newItem;
        currentHeldObject.SetActive(true);  // Activate the new item
    }

    private void UpdateHotbarUI() {
        // Update each hotbar slot's icon and keep the outlines visible
        for (int i = 0; i < hotbarSlots.Count; i++) {
            if (i < inventory.Count) {
                hotbarSlots[i].sprite = inventory[i].GetComponent<Item>().icon;  // Assumes items have an Item script with an icon field
                hotbarSlots[i].enabled = true;
            } else {
                hotbarSlots[i].enabled = false;  // Hide empty slot icons
            }

            // Ensure outline is always enabled
            if (hotbarOutlines.Count > i) {
                hotbarOutlines[i].enabled = true;
            }
        }
    }
}
