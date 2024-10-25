using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] InventorySystem inventorySystem;
    [SerializeField] Transform itemContainer;
    [SerializeField] LayerMask whatIsInteractable;
    [SerializeField] float range = 2f;

    private GameObject objectInHands = null;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit interactable, range, whatIsInteractable))
            {
                objectInHands = interactable.transform.gameObject;

                objectInHands.transform.SetParent(itemContainer);
                objectInHands.transform.localPosition = Vector3.zero;
                objectInHands.transform.localRotation = Quaternion.identity;

                if (objectInHands.transform.TryGetComponent(out Rigidbody rb))
                    rb.isKinematic = true;

                //add item to inventory
                inventorySystem.AddItem(objectInHands);
            }
        }
    }
}
