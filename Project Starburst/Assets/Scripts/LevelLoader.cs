using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
    [SerializeField] Animator animator;
    [SerializeField] float transitionTime = 1f;

    public void LoadMainMenu() {
        StartCoroutine(LoadLevel(0));
    }

    public void LoadOptions() {
        StartCoroutine(LoadLevel(1));
    }

    public void LoadCredits() {
        StartCoroutine(LoadLevel(2));
    }

    public void LoadLevelOne() {
        StartCoroutine(LoadLevel(3));
    }

    public void LoadWinScreen() {
        StartCoroutine(LoadLevel(4));
    }

    public void LoadGameOver() {   
        StartCoroutine(LoadLevel(5));
    }

    private IEnumerator LoadLevel(int sceneIndex) {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneIndex);
    }
}