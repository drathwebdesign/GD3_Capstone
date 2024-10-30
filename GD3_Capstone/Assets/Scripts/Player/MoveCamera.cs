using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform CameraTransform;

    void Update()
    {
        transform.position = CameraTransform.position;
        transform.rotation = CameraTransform.rotation;
    }
}
