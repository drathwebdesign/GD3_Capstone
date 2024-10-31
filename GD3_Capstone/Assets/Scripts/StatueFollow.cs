using UnityEngine;

public class StatueFollow : MonoBehaviour
{
    public Transform player;
    public float lookSpeed = 2.0f;
    public float maxLookDistance = 10f;

    private void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("Player is missing");
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= maxLookDistance)
        {
            LookAtPlayer();
        }
    }

    void LookAtPlayer()
    {
        Vector3 directionToPLayer = player.position - transform.position;
        directionToPLayer.x = 0;

        Quaternion targetRotation = Quaternion.LookRotation(directionToPLayer);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookSpeed * Time.deltaTime);  
    }
}
