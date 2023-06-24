using UnityEngine;

public class TrackingBullet : Projectile {
    [SerializeField] bool fired = true;
    private GameObject target;

    protected override void Start() {
        base.Start();
        if (!isPlayerProjectile) { target = Player.instance.gameObject; }
    }

    protected override void Update() {
        if (fired) {
            SetRotation();
            xMovementSpeed = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.z) * movementSpeed;
            yMovementSpeed = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.z) * movementSpeed * -1;

            transform.position += new Vector3(xMovementSpeed * Time.deltaTime, yMovementSpeed * Time.deltaTime, 0);
        }
    }

    public bool HasTarget() {
        return target != null;
    }

    public void HasFired() {
        fired = true;
    }

    public void SetTarget(GameObject target) {
        this.target = target;
    }

    private void SetRotation() {
        if (!target) {
            return;
        }
        var offset = new Vector2(
            transform.position.x - target.transform.position.x,
            transform.position.y - target.transform.position.y
        );
        var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 270);
    }
}
