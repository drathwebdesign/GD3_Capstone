using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public float upMaxRotation = 90f;
    [SerializeField] Transform playerTransform;

    float xRotation = 0f;
    void Start()
    {
        //if MouseLook is enabled lock the cursor and make it transparent
        if(enabled)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        //get input and apply sensitivity and make it frame independent
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //clamp up and down look rotation to upMaxRotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -upMaxRotation, upMaxRotation);

        //rotate upLook based on mouseY 
        transform.localRotation = Quaternion.Euler(new Vector3(xRotation, 0, 0));
        //rotate player body based on mouseX rot
        playerTransform.Rotate(Vector3.up * mouseX);
    }
}
