using UnityEngine;
using System.Collections.Generic;

public class TargetObjectActivator : MonoBehaviour {
    public GameObject[] targetObjects;  // Array to hold multiple objects
    [HideInInspector] public bool hasBeenActivated = false;

    public void ActivateTargets() {
        // Check if the objects have already been activated
        if (hasBeenActivated) return;

        // Activate each target object in the array
        foreach (GameObject target in targetObjects) {
            if (target != null) {
                target.SetActive(true);  // Activate each object
            }
        }

        hasBeenActivated = true;  // Ensure it only activates once
    }
}
