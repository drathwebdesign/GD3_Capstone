using UnityEngine;

public class StalkerSpawnTrigger : MonoBehaviour {
    [SerializeField] private TargetObjectActivator activator;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (activator != null && !activator.hasBeenActivated) {
                activator.ActivateTargets();
            }
        }
    }
}