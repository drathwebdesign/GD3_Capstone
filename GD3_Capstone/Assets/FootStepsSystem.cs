using UnityEngine;

public class FootStepsSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] HeadBobController headBobController;

    [Header("Audio")]
    [SerializeField] AudioSource playerAudioSource;
    [SerializeField] AudioClip[] footstepSounds;

    private bool once = false;

    void LateUpdate()
    {
        if (headBobController.controller.GetMovementVector().magnitude > 0.1f && headBobController.GetCameraTransform().localPosition.y < 0)
        {
            if (!once)
            {
                once = true;
                int index = Random.Range(0, footstepSounds.Length);
                playerAudioSource.PlayOneShot(footstepSounds[index]);
            }
        }
        else if (headBobController.GetCameraTransform().localPosition.y >= 0)
            once = false;
    }
}
