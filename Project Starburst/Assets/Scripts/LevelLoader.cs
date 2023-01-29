using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
    [SerializeField] Animator animator;
    [SerializeField] float transitionTime = 1f;
    [SerializeField] GameObject eolPlayerClone;

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
        StartCoroutine(WinCelebration());
    }

    public void LoadGameOver() {   
        StartCoroutine(LoadLevel(5));
    }

    private IEnumerator LoadLevel(int sceneIndex) {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneIndex);
    }

    private IEnumerator WinCelebration() {
        GameObject player = GameObject.Find("Player");
        player.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
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