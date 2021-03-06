using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    #region Methods
    // Called once per frame
    private void Update() {
        // Shoot a bullet when the user presses the fire button
        // which is typically lft mouse button
        if (Input.GetButtonDown("Fire1")) {
            Bullet bullet = SpawnBullet();

            // Add a force to the bullet in the player's up direction (forward)
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.AddForce(transform.up * m_bulletSpeed, ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// Instantiates a bullet
    /// </summary>
    /// <returns>The spawned bullet</returns>
    private Bullet SpawnBullet() {
        // Bullet's starting position with some offset on the horizontal axis relative
        // to the local player's sideways (right)
        Vector2 startingPosition =
            (Vector2)transform.position +
            (new Vector2(m_bulletFirePoint.x, 0f) * transform.right);

        // The offset on the vertical axis relative the local player's forward (up)
        Vector2 forwardOffset =
            new Vector2(m_bulletFirePoint.y, m_bulletFirePoint.y) * transform.up;

        // The bullet rotation based on the player's rotation
        Quaternion finalRotation =
            Quaternion.Euler(m_bulletPrefab.transform.rotation.eulerAngles +
            transform.rotation.eulerAngles);

        // Instantiate a bullet at the player position taking into account offset
        // and rotating the bullet based on the player's rotation.
        Bullet blt = Instantiate(m_bulletPrefab,
            startingPosition + forwardOffset,
            finalRotation);

        // Assign the bullet's owner to be this gameobject (the player)
        blt.Owner = tag;

        return blt;
    }
    #endregion

    #region Member variables
    [SerializeField]
    private Bullet m_bulletPrefab;

    [SerializeField]
    [Tooltip("The offset from the player where the bullet will be fired from")]
    private Vector2 m_bulletFirePoint = new Vector2(0.015f, 0.2f);

    [SerializeField]
    private float m_bulletSpeed = 2f;
    #endregion
}
