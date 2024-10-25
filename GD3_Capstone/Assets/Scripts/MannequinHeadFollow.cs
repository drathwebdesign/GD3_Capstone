using UnityEngine;

public class MannequinHeadFollow : MonoBehaviour {
    public Transform headTransform;  // Assign the head transform of the mannequin
    public Transform playerTransform; // Assign the player's transform (likely the camera)

    private MannequinFOVDetector fovDetector; // Reference to the FOV Detector script

    void Start() {
        // Get reference to the FOV detector on the player
        fovDetector = playerTransform.GetComponent<MannequinFOVDetector>();
    }

    void Update() {
        // If the mannequin is not in view, rotate the head to face the player
        if (!fovDetector.IsMannequinInView(transform)) {
            RotateHeadTowardsPlayer();
        }
    }

    void RotateHeadTowardsPlayer() {
        Vector3 directionToPlayer = playerTransform.position - headTransform.position;
        directionToPlayer.y = 0; // Optionally, keep the head rotation only on the horizontal axis

        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        headTransform.rotation = Quaternion.Slerp(headTransform.rotation, lookRotation, Time.deltaTime * 2f); // Smooth rotation
    }
}