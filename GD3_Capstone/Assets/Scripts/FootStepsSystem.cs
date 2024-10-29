using UnityEngine;

public class FootStepsSystem : MonoBehaviour {
    [Header("References")]
    [SerializeField] HeadBobController headBobController;
    [SerializeField] PlayerMovement playerMovement; // Reference to PlayerMovement script

    [Header("Audio")]
    [SerializeField] AudioSource playerAudioSource;
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

        int index = Random.Range(0, footstepArray.Length);
        previousIndex = index;

        if (previousIndex == index && index < footstepArray.Length - 1)
            index++;

        playerAudioSource.PlayOneShot(footstepArray[index]);
    }
}
