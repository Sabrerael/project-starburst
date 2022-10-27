using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] int healthPoints = 100;

    public int GetHealthPoints() { return healthPoints; }

    public void ModifyHealthPoints(int value) { 
        healthPoints += value;
    }
}
