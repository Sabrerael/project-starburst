using UnityEngine;

public class Player : MonoBehaviour {
    private int totalScore = 0;

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy Projectile") {
            Debug.Log("Enemy Projectile hit " + name);
            int damage = other.gameObject.GetComponent<EnemyProjectile>().GetDamage();
            GetComponent<Health>().ModifyHealthPoints(-damage);
            Destroy(other.gameObject);
        }
    }

    public int GetTotalScore() { return totalScore; }
    public void AddToTotalScore(int value) { totalScore += value; }

}
