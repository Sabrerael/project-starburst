using UnityEngine;

public class BoomerangBullet : Projectile {
    [SerializeField] float timeToGoForward = 2f;
    [SerializeField] float timeToReverse = 0.5f;
    
    private float timer = 0;
    private float turningTimer = 0;
    private float actualMovementSpeed;

    protected override void Start() {
        base.Start();
        actualMovementSpeed = movementSpeed;
    }

    protected override void Update() {
        base.Update();
        timer += Time.deltaTime;
        if (turningTimer >= timeToReverse) {
            return;
        } else if (timer >= timeToGoForward) {
            turningTimer += Time.deltaTime;
            actualMovementSpeed = Mathf.Lerp(movementSpeed, -movementSpeed, turningTimer / timeToReverse);
            xMovementSpeed = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.z) * actualMovementSpeed * -1;
            yMovementSpeed = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.z) * actualMovementSpeed;
        }
    }

}
