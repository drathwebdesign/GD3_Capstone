using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventorySystem : MonoBehaviour {
    public List<GameObject> inventory;
    private GameObject currentHeldObject;
    private int maxHotbarItems = 8;
    [SerializeField] Transform itemContainer;  // ItemContainer positioned in front of the player

    public List<Image> hotbarSlots;  // List of UI images for each hotbar slot

    private void Start() {
        inventory = new List<GameObject>();
    }

    void Update() {
        for (int i = 0; i < maxHotbarItems; i++) {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i) && i < inventory.Count) {
                SetCurrentHeldObject(inventory[i]);
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

    public void SetCurrentHeldObject(GameObject newItem) {
        if (currentHeldObject) {
            currentHeldObject.SetActive(false);
        }

        // Move item to itemContainer in front of player
        newItem.transform.SetParent(itemContainer);
        newItem.transform.localPosition = Vector3.zero;  // Center it in itemContainer
        newItem.transform.localRotation = Quaternion.identity;

        currentHeldObject = newItem;
        currentHeldObject.SetActive(true);  // Activate the new item
    }

    private void UpdateHotbarUI() {
        // Update each hotbar slot's icon based on the current inventory
        for (int i = 0; i < hotbarSlots.Count; i++) {
            if (i < inventory.Count) {
                hotbarSlots[i].sprite = inventory[i].GetComponent<Item>().icon;  // Assumes items have an Item script with an icon field
                hotbarSlots[i].enabled = true;
            } else {
                hotbarSlots[i].enabled = false;  // Hide empty slots
            }
        }
    }
}
