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
        //Works for now I guess
        if (Input.GetKeyDown(KeyCode.Alpha1) && inventory.Count > 0)
            SetCurrentHeldObject(inventory[0]);
        if (Input.GetKeyDown(KeyCode.Alpha2) && inventory.Count > 1)
            SetCurrentHeldObject(inventory[1]);
        if (Input.GetKeyDown(KeyCode.Alpha3) && inventory.Count > 2)
            SetCurrentHeldObject(inventory[2]);
        if (Input.GetKeyDown(KeyCode.Alpha4) && inventory.Count > 3)
            SetCurrentHeldObject(inventory[3]);
        if (Input.GetKeyDown(KeyCode.Alpha5) && inventory.Count > 4)
            SetCurrentHeldObject(inventory[4]);
        if (Input.GetKeyDown(KeyCode.Alpha6) && inventory.Count > 5)
            SetCurrentHeldObject(inventory[5]);
        if (Input.GetKeyDown(KeyCode.Alpha7) && inventory.Count > 6)
            SetCurrentHeldObject(inventory[6]);
        if (Input.GetKeyDown(KeyCode.Alpha8) && inventory.Count > 7)
            SetCurrentHeldObject(inventory[7]);
        if (Input.GetKeyDown(KeyCode.Alpha9) && inventory.Count > 8)
            SetCurrentHeldObject(inventory[8]);
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
