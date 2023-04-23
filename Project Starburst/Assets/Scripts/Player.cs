using UnityEngine;

public class Player : MonoBehaviour {
    public static Player instance = null;

    private int totalScore = 0;
    private int comboCounter = 0;

    // Unity Functions
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public int GetTotalScore() { return totalScore; }

    public void AddToTotalScore(int value) { 
        value += (comboCounter * 10);
        comboCounter++; 
        totalScore += value; 
    }

    public void ResetComboCounter() { comboCounter = 0; }

    public void ResetPlayer() {
        transform.position = new Vector3(0, -4, 0);
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        transform.GetChild(4).gameObject.SetActive(true);
    }

}
