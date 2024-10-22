using UnityEngine;

public class FootStepsSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] HeadBobController headBobController;

    [Header("Audio")]
    [SerializeField] AudioSource playerAudioSource;
    [SerializeField] AudioClip[] dirtFootStepsArray;

    private bool once = false;
    private int previousIndex = 0;
    void LateUpdate()
    {
        //if player is moving and camera is going down play sounds once per step
        if (headBobController.controller.GetMovementVector().magnitude > 0.1f && headBobController.GetCameraTransform().localPosition.y < 0)
        {
            //use once to play sound once camera is negative
            if (!once)
            {
                once = true;
                PlaySoundsBySurface();
            }
        }
        //reset bool to play sound again
        else if (headBobController.GetCameraTransform().localPosition.y >= 0)
            once = false;
    }
    void PlaySoundsBySurface()
    {
        //get random index from array
        int index = Random.Range(0, dirtFootStepsArray.Length);
        previousIndex = index;
        
        //don't play same footstep twice
        if (previousIndex == index && index < dirtFootStepsArray.Length - 1) 
            index++;

        playerAudioSource.PlayOneShot(dirtFootStepsArray[index]);
    }
}
