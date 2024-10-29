using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour {
    public static SoundFXManager Instance;

    // Array to hold different AudioSource prefabs
    [SerializeField] private AudioSource[] soundFXObjects;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    // Method that plays a sound using a specific soundFXObject prefab selected by index
    public void PlaySoundFXClip(int soundFXIndex, AudioClip audioClip, Transform spawnTransform, float volume) {
        // Ensure the index is within the bounds of the array
        if (soundFXIndex < 0 || soundFXIndex >= soundFXObjects.Length) {
            Debug.LogError("SoundFX index out of bounds!");
            return;
        }

        // Select the soundFX prefab based on the index
        AudioSource audioSourcePrefab = soundFXObjects[soundFXIndex];

        // Spawn the AudioSource prefab
        //   AudioSource audioSource = Instantiate(audioSourcePrefab, spawnTransform.position, Quaternion.identity);

        //find the player and make the audiosource prefab a child of the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        AudioSource audioSource = Instantiate(audioSourcePrefab, spawnTransform.position, Quaternion.identity, player.transform);

        // Assign the audio clip
        audioSource.clip = audioClip;

        // Assign volume
        audioSource.volume = volume;

        // Play the sound
        audioSource.Play();

        // Destroy the sound object after the clip finishes playing
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }

    // Overloaded method to handle an array of AudioClips (randomly selects one) and play the specified soundFXObject prefab by index
    public void PlayRandomSoundFXClip(int soundFXIndex, AudioClip[] audioClips, Transform spawnTransform, float volume) {
        // Ensure the index is within bounds
        if (soundFXIndex < 0 || soundFXIndex >= soundFXObjects.Length) {
            Debug.LogError("SoundFX index out of bounds!");
            return;
        }

        // Select the soundFX prefab based on the index
        AudioSource audioSourcePrefab = soundFXObjects[soundFXIndex];

        // Randomly select a clip
        int random = Random.Range(0, audioClips.Length);

        // Spawn the AudioSource prefab
        AudioSource audioSource = Instantiate(audioSourcePrefab, spawnTransform.position, Quaternion.identity);

        // Assign random audio clip
        audioSource.clip = audioClips[random];

        // Assign volume
        audioSource.volume = volume;

        // Play the sound
        audioSource.Play();

        // Destroy after the clip finishes playing
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }
}