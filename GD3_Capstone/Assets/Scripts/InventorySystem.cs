using UnityEngine;
using System.Collections.Generic;

public class InventorySystem : MonoBehaviour
{
    public List<GameObject> inventory;
    private GameObject currentHeldObject;

    private void Start()
    {
        inventory = new List<GameObject>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && inventory.Count > 0)
            SetCurrentHeldObject(inventory[0]);
        if (Input.GetKeyDown(KeyCode.Alpha2) && inventory.Count > 1)
            SetCurrentHeldObject(inventory[1]);
        if (Input.GetKeyDown(KeyCode.Alpha3) && inventory.Count > 2)
            SetCurrentHeldObject(inventory[2]);
    }
    public void AddItem(GameObject item)
    {
        inventory.Add(item);
        item.SetActive(false);
    }
    public void SetCurrentHeldObject(GameObject newItem)
    {
            if (!currentHeldObject)
            {
                currentHeldObject = newItem;
                currentHeldObject.SetActive(true);
                return;
            }
            currentHeldObject.SetActive(false);
            currentHeldObject = newItem;
            currentHeldObject.SetActive(true);
    }
}
