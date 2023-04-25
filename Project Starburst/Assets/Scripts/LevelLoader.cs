using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
    [SerializeField] Animator animator;
    [SerializeField] float transitionTime = 1f;
    [SerializeField] GameObject eolPlayerClone;

    private int score;

    private void DestroyPlayerObjects() {
        var player = GameObject.FindGameObjectWithTag("Player");
        score = player.GetComponent<Player>().GetTotalScore();
        if (player) {
            GameObject.Destroy(GameObject.Find("Pause Menu"));
            GameObject.Destroy(GameObject.Find("HUD"));
            GameObject.Destroy(player);
        }
    }

    public void LoadMainMenu() {
        Time.timeScale = 1;
        DestroyPlayerObjects();
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

    public void LoadLevelTwo() {
        StartCoroutine(WinCelebration(4));
    }

    public void LoadWinScreen() {
        DestroyPlayerObjects();
        StartCoroutine(WinCelebration(5));
    }

    public void LoadGameOver() {
        DestroyPlayerObjects();
        StartCoroutine(LoadLevel(6));
    }

    public void QuitGame() {
        Application.Quit();
    }

    private IEnumerator LoadLevel(int sceneIndex) {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneIndex);
    }

    private IEnumerator WinCelebration(int levelIndex) {
        GameObject player = GameObject.Find("Player");
        player.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        player.transform.GetChild(4).gameObject.SetActive(false);
        player.transform.GetChild(5).gameObject.SetActive(false);
        player.transform.GetChild(6).gameObject.SetActive(false);
        // TODO Particles are still showing up
        player.transform.GetChild(3).gameObject.SetActive(false);
        GameObject playerClone = Instantiate(eolPlayerClone, new Vector3(0, -1, 0), Quaternion.identity);
        yield return new WaitForSeconds(1);
        playerClone.GetComponent<Animator>().SetTrigger("Fly");
        yield return new WaitForSeconds(2);
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(4);
    }
}