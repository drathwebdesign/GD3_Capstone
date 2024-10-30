using UnityEngine;

public class FootStepsSystem : MonoBehaviour {
    [Header("References")]
    [SerializeField] HeadBobController headBobController;
    [SerializeField] PlayerMovement playerMovement; // Reference to PlayerMovement script

    [Header("Audio")]
    [SerializeField] AudioClip[] outdoorFootStepsArray; // Outdoor footsteps
    [SerializeField] AudioClip[] indoorFootStepsArray;  // Indoor footsteps

    private bool once = false;
    private int previousIndex = 0;

    void LateUpdate() {
        if (headBobController.controller.GetMovementVector().magnitude > 0.1f && headBobController.GetCameraTransform().localPosition.y < 0) {
            if (!once) {
                once = true;
                PlaySoundsBySurface();
            }
        } else if (headBobController.GetCameraTransform().localPosition.y >= 0) {
            once = false;
        }
    }

    void PlaySoundsBySurface() {
        AudioClip[] footstepArray = playerMovement.isIndoors ? indoorFootStepsArray : outdoorFootStepsArray;

        // Choose a random index and ensure it's not the same as the last one
        int index = Random.Range(0, footstepArray.Length);
        if (previousIndex == index && index < footstepArray.Length - 1)
            index++;
        previousIndex = index;

        // Play the selected sound through the SoundFXManager
        SoundFXManager.Instance.PlaySoundFXClip(0, footstepArray[index], transform, 1f);
    }
}