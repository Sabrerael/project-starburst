using UnityEngine;

public class Player : MonoBehaviour {
    private int totalScore = 0;

    public int GetTotalScore() { return totalScore; }
    public void AddToTotalScore(int value) { totalScore += value; }

}
