using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    public GameObject player;  // Reference to the Player GameObject
    float autoSaveInterval = 60f;

    void Start() {
        // Check if a save file exists (using the generated save name)
        PlayerData loadedData = SaveSystem.LoadPlayer(GameManager.instance.saveFileName);
        if (loadedData != null) {
            Debug.Log("Player data loaded.");

            // Set player's position to the saved data
            Vector3 loadedPosition = loadedData.playerPosition.GetPosition();
            player.transform.position = loadedPosition;

            Debug.Log("Player position set to: " + loadedPosition);
        } else {
            Debug.LogWarning("No player data found. Starting a new game.");
            // Optionally set the player to a default starting position here
        }

        // Start autosaving the player’s position every autoSaveInterval seconds
        InvokeRepeating("SavePlayerPosition", autoSaveInterval, autoSaveInterval);
    }

    public void SavePlayerPosition() {
        Vector3 currentPosition = player.transform.position;

        PlayerData updatedPlayerData = new PlayerData(GameManager.instance.saveFileName, currentPosition);
        SaveSystem.SavePlayer(updatedPlayerData);
        Debug.Log("Auto-saved player data at position: " + currentPosition);
    }

    private void OnApplicationQuit() {
        SavePlayerPosition();
#if (UNITY_EDITOR)
        EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif
    }
}