using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Transform itemContainer;
    public LayerMask whatIsInteractable;
    public float range = 2f;

    private bool handsOccupied = false;
    private GameObject objectInHands = null;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit interactable, range, whatIsInteractable))
            {
                handsOccupied = true;
                objectInHands = interactable.transform.gameObject;

                objectInHands.transform.SetParent(itemContainer);
                objectInHands.transform.localPosition = Vector3.zero;
                objectInHands.transform.localRotation = Quaternion.identity;

                objectInHands.transform.GetComponent<Rigidbody>().isKinematic = true;
                objectInHands.transform.gameObject.SetActive(false);

                //add item to inventory
            }
        }
    }
}
