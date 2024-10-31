using UnityEngine;

public class ButtonSoundManager : MonoBehaviour
{
    public AudioClip buttonClickSound;
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
    }

    public void PlayButtonClickSound()
    {
        audioSource.PlayOneShot(buttonClickSound);
    }
}
