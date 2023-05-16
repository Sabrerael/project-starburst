using TMPro;
using UnityEngine;

public class WinScreenScore : MonoBehaviour {
    [SerializeField] TextMeshProUGUI scoreValue;
    [SerializeField] TextMeshProUGUI highscoreValue;

    private void Start() {
        scoreValue.text = PlayerPrefs.GetInt("RECENT_SCORE", 0).ToString("000000000");
        highscoreValue.text = PlayerPrefs.GetInt("HIGH_SCORE", 0).ToString("000000000");
    }
}
