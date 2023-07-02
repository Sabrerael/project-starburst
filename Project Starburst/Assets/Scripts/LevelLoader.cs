using Steamworks;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
    [SerializeField] Animator animator;
    [SerializeField] float transitionTime = 1f;
    [SerializeField] GameObject eolPlayerClone;

	private CallResult<LeaderboardFindResult_t> steamLeaderboard;
    private CallResult<LeaderboardScoreUploaded_t> scoreUploaded;
    private SteamLeaderboard_t leaderboard;

	private void OnEnable() {
		if (SteamManager.Initialized) {
			steamLeaderboard = CallResult<LeaderboardFindResult_t>.Create(OnSteamLeaderboard);
            scoreUploaded = CallResult<LeaderboardScoreUploaded_t>.Create(OnLeaderboardScoreUploaded);
		}
	}

    private void Start() {
        SteamAPICall_t handle = SteamUserStats.FindLeaderboard("High Score");
		steamLeaderboard.Set(handle);
		Debug.Log("Called FindLeaderboard()");
    }

    private void DestroyPlayerObjects() {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player) {
            var score = player.GetComponent<Player>().GetTotalScore();
            var highScore = PlayerPrefs.GetInt("HIGH_SCORE", 0);
            PlayerPrefs.SetInt("RECENT_SCORE", score);
            PlayerPrefs.SetInt("HIGH_SCORE", Mathf.Max(score, highScore));
            if (SteamManager.Initialized) {
                Debug.Log("");
                SteamAPICall_t handle = SteamUserStats.UploadLeaderboardScore(leaderboard,
                                                                ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodKeepBest,
                                                                score,
                                                                null,
                                                                0);
                scoreUploaded.Set(handle);
                Debug.Log("Called UploadLeaderboardScore");
            }
            GameObject.Destroy(GameObject.FindObjectOfType<PauseMenu>(true).gameObject);
            GameObject.Destroy(GameObject.Find("HUD"));
            GameObject.Destroy(player);
        }
    }

    public void LoadMainMenu() {
        Time.timeScale = 1;
        StartCoroutine(LoadLevel(0));
        DestroyPlayerObjects();
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

    public void LoadLevelThree() {
        StartCoroutine(WinCelebration(5));
    }

    public void LoadLevelFour() {
        StartCoroutine(WinCelebration(6));
    }

    public void LoadLevelFive() {
        StartCoroutine(WinCelebration(7));
    }

    public void LoadLevelSix() {
        StartCoroutine(WinCelebration(8));
    }

    public void LoadLevelSeven() {
        StartCoroutine(WinCelebration(9));
    }

    public void LoadEndlessMode() {
        StartCoroutine(WinCelebration(12));
    }

    public void LoadWinScreen() {
        StartCoroutine(WinCelebration(10));
    }

    public void LoadGameOver() {
        DestroyPlayerObjects();
        StartCoroutine(LoadLevel(11));
    }

    public void QuitGame() {
        SteamAPI.Shutdown();
        Application.Quit();
    }

	private void OnSteamLeaderboard(LeaderboardFindResult_t pCallback, bool bIOFailure) {
		if (pCallback.m_bLeaderboardFound != 1 || bIOFailure) {
			Debug.Log("There was an error retrieving the Steam Leaderboard.");
		}
		else {
			Debug.Log("Steam Leaderboard name: " + pCallback.m_hSteamLeaderboard);
            leaderboard = pCallback.m_hSteamLeaderboard;
		}
	}

    private void OnLeaderboardScoreUploaded(LeaderboardScoreUploaded_t pCallback, bool bIOFailure) {
		if (pCallback.m_bSuccess != 1 || bIOFailure) {
			Debug.Log("There was an error uploading to the Steam Leaderboard.");
		}
		else {
			Debug.Log("Score of " + pCallback.m_nScore + " uploaded to " + pCallback.m_hSteamLeaderboard + " leaderboard");
		}
	} 

    private IEnumerator LoadLevel(int sceneIndex) {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneIndex);
    }

    private IEnumerator WinCelebration(int levelIndex) {
        GameObject player = GameObject.Find("Player");
        player.transform.GetChild(1).GetComponent<BulletMagnet>().ClearBulletArray();
        player.GetComponent<BoxCollider2D>().enabled = false;
        player.GetComponent<PlayerController>().enabled = false;
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
        SceneManager.LoadScene(levelIndex);
        if (levelIndex == 5) { DestroyPlayerObjects(); }
    }
}