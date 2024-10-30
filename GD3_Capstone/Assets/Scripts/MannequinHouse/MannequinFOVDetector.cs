using UnityEngine;

public class MannequinFOVDetector : MonoBehaviour {
    public float fieldOfView = 95f; // Player's FOV in degrees
    public Transform playerCamera;  // Assign the player's camera transform in the inspector

    // Function to check if a mannequin is within the player's FOV
    public bool IsMannequinInView(Transform mannequin) {
        Vector3 directionToMannequin = mannequin.position - playerCamera.position;
        directionToMannequin.Normalize();

        float angle = Vector3.Angle(playerCamera.forward, directionToMannequin);

        if (angle < fieldOfView / 2f) {
            return true;
        }
        return false;
    }
}