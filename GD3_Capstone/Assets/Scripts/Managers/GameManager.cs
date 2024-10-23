using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public string saveFileName = "last_saved_game";  // Fixed save file name for continue

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    // This will be called when "New Game" is clicked
    public void StartNewGame() {
        // Optionally, generate a new save file name for a fresh start, or reset the existing one
        saveFileName = "last_saved_game";  // Use a fixed name for saving progress
        Debug.Log("Starting new game with save file: " + saveFileName);

        // Create a new player with a default starting position and save
        PlayerData newPlayerData = new PlayerData(saveFileName, new Vector3(4f, -0.51f, -30f));
        SaveSystem.SavePlayer(newPlayerData);

        // Load the game scene (e.g., Scene index 1 is your game scene)
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    // This will be called when "Continue" is clicked
    public void ContinueGame() {
        Debug.Log("Attempting to continue game with save file: " + saveFileName);

        // Check if the save file exists and load the game if it does
        if (SaveSystem.DoesSaveExist(saveFileName)) {
            // Load the game scene where the player left off
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        } else {
            Debug.LogWarning("No save file exists to continue.");
        }
    }
}
