using UnityEngine;

public class CollisionSoundManager : MonoBehaviour
{
    [SerializeField] AudioSource collisionAudioSource;

    public float collisionVolume = 0.7f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collisionAudioSource = GetComponent<AudioSource>();
        collisionAudioSource.playOnAwake = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collisionAudioSource != null && collisionAudioSource.clip != null)
        {
            collisionAudioSource.volume = collisionVolume;
            collisionAudioSource.Play();
        }
    }
}
