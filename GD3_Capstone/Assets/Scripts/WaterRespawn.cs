using UnityEngine;

public class WaterTriggerRespawn : MonoBehaviour {
    public Transform player;             // Reference to the player transform
    public float targetYLevel = -5f;     // Target Y level for respawn
    public float searchRadius = 10f;     // Radius to search for ground points around the player

    private void OnTriggerEnter(Collider other) {
        // Log the name of the collider we entered
        Debug.Log("Entered Trigger with: " + other.gameObject.name);

        // Check if the collider we entered is tagged as "Water"
        if (other.CompareTag("Water")) {
            Debug.Log("Water detected. Triggering respawn...");
            RespawnPlayer();
        }
    }

    void RespawnPlayer() {
        // Find the closest point on the ground within the search radius at target Y level
        Vector3 closestGroundPoint = FindClosestGroundPoint(player.position);

        // Log if a ground point was found
        if (closestGroundPoint != Vector3.zero) {
            Debug.Log("Respawning player to closest ground point at: " + closestGroundPoint);
            player.position = closestGroundPoint;
            player.rotation = Quaternion.identity; // Reset rotation if needed
        } else {
            Debug.LogWarning("No valid ground point found for respawn.");
        }
    }

    Vector3 FindClosestGroundPoint(Vector3 currentPosition) {
        Vector3 closestPoint = Vector3.zero;
        float closestDistance = Mathf.Infinity;

        // Loop through points within the search radius
        for (float x = -searchRadius; x <= searchRadius; x += 1f) {
            for (float z = -searchRadius; z <= searchRadius; z += 1f) {
                // Calculate the check position at the target Y level
                Vector3 checkPosition = new Vector3(currentPosition.x + x, targetYLevel, currentPosition.z + z);

                // Cast a ray downward to detect ground
                if (Physics.Raycast(checkPosition + Vector3.up * 10f, Vector3.down, out RaycastHit hit, 20f)) {
                    if (hit.collider.CompareTag("Ground")) {
                        // Calculate distance to this point
                        float distance = Vector3.Distance(currentPosition, hit.point);
                        if (distance < closestDistance) {
                            closestDistance = distance;
                            closestPoint = new Vector3(hit.point.x, targetYLevel, hit.point.z); // Set Y level to target
                        }
                    }
                }
            }
        }

        // Log if no ground point was found within the search radius
        if (closestPoint == Vector3.zero) {
            Debug.LogWarning("No ground point found within search radius.");
        }

        return closestPoint;
    }
}
