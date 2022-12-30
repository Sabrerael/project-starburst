using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
    public void LoadMainMenu() {
        SceneManager.LoadScene(0);
    }

    public void LoadOptions() {
        SceneManager.LoadScene(1);
    }

    public void LoadCredits() {
        SceneManager.LoadScene(2);
    }

    public void LoadLevelOne() {
        SceneManager.LoadScene(3);
    }

    public void LoadWinScreen() {
        SceneManager.LoadScene(4);
    }

    public void LoadGameOver() {   
        SceneManager.LoadScene(5);
    }
}