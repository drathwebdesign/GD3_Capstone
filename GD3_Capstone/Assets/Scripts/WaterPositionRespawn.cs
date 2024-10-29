using UnityEngine;

public class WaterPositionRespawn : MonoBehaviour {
    public Transform player;                 // Reference to the player transform
    public float waterLevel = -5.5f;           // Water level Y coordinate
    public float respawnHeightAboveWater = 2f; // Height above water level for safe respawn
    public float initialSearchRadius = 10f;  // Initial radius to search for ground points around the player
    public float searchIncrement = 5f;       // Incremental radius increase to search further inland
    public float maxSearchRadius = 50f;      // Max search radius to avoid endless searching
    public float minRespawnDistance = 2f;    // Minimum acceptable distance for respawn point
    public LayerMask waterLayer;             // LayerMask for water layer
    public LayerMask groundLayer;            // LayerMask for ground layer

    private bool isRespawning = false;       // Flag to disable controls during respawn

    private void LateUpdate() {
        // Check if the player is below the water level and not already respawning
        if (player.position.y < waterLevel && !isRespawning) {
            isRespawning = true;
            DisablePlayerControls();
            RespawnPlayer();
        }
    }

    void RespawnPlayer() {
        Vector3 closestGroundPoint = FindClosestSafeGroundPoint(player.position);

        // If a valid ground point is found
        if (closestGroundPoint != Vector3.zero) {
            player.position = closestGroundPoint;
            player.rotation = Quaternion.identity; // Reset rotation if needed

            // Re-enable player controls after respawn
            EnablePlayerControls();
            isRespawning = false;
        } else {
            EnablePlayerControls();
            isRespawning = false;
        }
    }

    Vector3 FindClosestSafeGroundPoint(Vector3 currentPosition) {
        float currentRadius = initialSearchRadius;
        Vector3 closestPoint = Vector3.zero;
        float closestDistance = Mathf.Infinity;

        while (currentRadius <= maxSearchRadius) {
            bool foundSafePoint = false;

            for (float x = -currentRadius; x <= currentRadius; x += 3f) {   // Increased increment to 3f for fewer checks
                for (float z = -currentRadius; z <= currentRadius; z += 3f) {
                    Vector3 checkPosition = new Vector3(currentPosition.x + x, waterLevel + respawnHeightAboveWater, currentPosition.z + z);

                    // Cast a ray downward to detect ground at the check position
                    if (Physics.Raycast(checkPosition + Vector3.up * 10f, Vector3.down, out RaycastHit hit, 20f, groundLayer)) {
                        // Ensure the hit point is above water level and far enough inland (not in water)
                        if (hit.point.y >= waterLevel + respawnHeightAboveWater) {
                            // Check for water around the point to ensure it's on dry land
                            if (!Physics.CheckSphere(hit.point, 1f, waterLayer)) {
                                float distance = Vector3.Distance(currentPosition, hit.point);
                                if (distance < closestDistance) {
                                    closestDistance = distance;
                                    closestPoint = new Vector3(hit.point.x, hit.point.y + respawnHeightAboveWater, hit.point.z);
                                    foundSafePoint = true;

                                    // If the point is close enough, skip further checks
                                    if (closestDistance <= minRespawnDistance) {
                                        return closestPoint;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (foundSafePoint) {
                break; // Stop searching if a valid dry ground point is found
            }

            currentRadius += searchIncrement; // Expand the search radius and try again
        }

        return closestPoint;
    }

    void DisablePlayerControls() {
        PlayerMovement movementScript = player.GetComponent<PlayerMovement>();
        if (movementScript != null) {
            movementScript.enabled = false;
        }
    }

    void EnablePlayerControls() {
        PlayerMovement movementScript = player.GetComponent<PlayerMovement>();
        if (movementScript != null) {
            movementScript.enabled = true;
        }
    }
}
