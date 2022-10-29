using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] int healthPoints = 100;

    private int totalHealthPoints;
    private int currentHealthPoints;

    private void Start() {
        totalHealthPoints = healthPoints;
        currentHealthPoints = healthPoints;
    }

    public int GetTotalHealthPoints() { return totalHealthPoints; }
    public int GetCurrentHealthPoints() { return currentHealthPoints; }
    public float GetFraction() { return (float)currentHealthPoints / (float)totalHealthPoints; }

    public void ModifyHealthPoints(int value) { 
        currentHealthPoints += value;
        if (currentHealthPoints <= 0) {
            Death();
        }
    }

    private void Death() {
        Destroy(gameObject);
        if (tag != "Player") {
            GetComponent<Enemy>().AddScoreToPlayer();
        }
    }
}
