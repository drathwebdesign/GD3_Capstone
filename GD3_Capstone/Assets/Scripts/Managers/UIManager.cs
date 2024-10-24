using UnityEngine;

public class UIManager : MonoBehaviour {
    public void OnNewGameButton() {
        GameManager.instance.StartNewGame();
    }

    public void OnContinueButton() {
        GameManager.instance.ContinueGame();
    }
}