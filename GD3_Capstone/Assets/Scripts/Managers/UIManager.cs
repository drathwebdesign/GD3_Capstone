using UnityEngine;

public class UIManager : MonoBehaviour {
    public GameObject confirmationPanel; // The panel that asks for confirmation to overwrite
    public GameObject mainMenuPanel;     // The main menu panel

    // This method is called when the "New Game" button is pressed
    public void OnNewGameButton() {
        // Check if a save file exists before starting a new game
        if (SaveSystem.DoesSaveExist(GameManager.instance.saveFileName)) {
            // If a save file exists, show the confirmation panel
            confirmationPanel.SetActive(true);
            mainMenuPanel.SetActive(false);  // Hide the main menu when confirmation is shown
        } else {
            // If no save file exists, just start a new game immediately
            GameManager.instance.StartNewGame();
        }
    }

    // Called when the player confirms to overwrite the existing save
    public void OnConfirmOverwrite() {
        // Start a new game and overwrite the existing save
        GameManager.instance.StartNewGame();
        confirmationPanel.SetActive(false);  // Hide the confirmation panel
        mainMenuPanel.SetActive(false);      // Optionally, you can keep the main menu hidden
    }

    // Called when the player cancels overwriting the existing save
    public void OnCancelOverwrite() {
        // Hide the confirmation panel and return to the main menu
        confirmationPanel.SetActive(false);
        mainMenuPanel.SetActive(true);  // Show the main menu again
    }

    // This method is called when the "Continue" button is pressed
    public void OnContinueButton() {
        // Check if a save file exists before continuing the game
        if (SaveSystem.DoesSaveExist(GameManager.instance.saveFileName)) {
            GameManager.instance.ContinueGame();
        } else {
            Debug.LogWarning("No save file exists to continue.");
        }
    }
}