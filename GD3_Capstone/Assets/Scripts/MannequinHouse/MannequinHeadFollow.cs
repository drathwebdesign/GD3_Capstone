using UnityEngine;

public class MannequinHeadFollow : MonoBehaviour {
    public Transform headTransform;  // Assign the head transform of the mannequin
    private Transform playerTransform; // Player's transform will be found automatically
    private MannequinFOVDetector fovDetector; // Reference to the FOV Detector script

    void Start() {
        // Find the player transform by tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null) {
            playerTransform = player.transform;
            fovDetector = playerTransform.GetComponent<MannequinFOVDetector>();
        } else {
            Debug.LogError("Player with tag 'Player' not found. Make sure a player object is tagged correctly.");
        }
    }

    void Update() {
        // If the mannequin is not in view, rotate the head to face the player
        if (playerTransform != null && fovDetector != null && !fovDetector.IsMannequinInView(transform)) {
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
