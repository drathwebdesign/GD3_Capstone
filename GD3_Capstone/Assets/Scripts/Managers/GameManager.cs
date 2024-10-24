using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public string saveFileName;

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
        // Generate a new save file name based on the current time
        saveFileName = "last_saved_game";
        Debug.Log("Starting new game with save file: " + saveFileName);

        // Optionally set the player's position to a default start position
        PlayerData newPlayerData = new PlayerData(saveFileName, new Vector3(4f, -0.51f, -30f));
        SaveSystem.SavePlayer(newPlayerData);

        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    // This will be called when "Continue" is clicked
    public void ContinueGame() {
        // Use the existing save file name (if available)
        saveFileName = "last_saved_game";  // Could be stored somewhere like PlayerPrefs
        Debug.Log("Continuing game with save file: " + saveFileName);

        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}