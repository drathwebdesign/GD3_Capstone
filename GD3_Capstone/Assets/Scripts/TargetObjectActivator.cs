using UnityEngine;

public class TargetObjectActivator : MonoBehaviour {
    public GameObject targetObject;  // The object that will be activated upon interaction
    [HideInInspector] public bool hasBeenActivated = false;
}