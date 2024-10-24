using UnityEngine;

public class CrawlerAI : MonoBehaviour {
    [SerializeField] AudioClip[] roarSoundClips;
    //[SerializeField] AudioClip roarSoundClip;
    private float cooldownTime = 5f;  // Time in seconds between sounds
    private float timeSinceLastRoar = 0f;  // Tracks time since the last sound was played

    void Start() {
    }

    void Update() {
        timeSinceLastRoar += Time.deltaTime; // Increment the time elapsed since last roar

        if (timeSinceLastRoar >= cooldownTime) // Check if cooldown time has passed
        {
            SoundFXManager.Instance.PlayRandomSoundFXClip(0, roarSoundClips, transform, 1f);
            timeSinceLastRoar = 0f;  // Reset the timer
        }
        //SoundFXManager.Instance.PlaySoundFXClip(1, roarSoundClip, transform, 1f);
    }
}