using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    #region Methods
    // Called once per frame
    private void Update() {
        // Need access to fire point
        if (!m_bulletFirePoint)
            return;

        // Shoot a bullet when the user presses the fire button
        // which is typically lft mouse button
        if (Input.GetButtonDown("Fire1")) {
            Bullet bullet = SpawnBullet();

            // Add a force to the bullet in the fire point's direction
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.AddForce(m_bulletFirePoint.up * m_bulletSpeed, ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// Instantiates a bullet
    /// </summary>
    /// <returns>The spawned bullet</returns>
    private Bullet SpawnBullet() {
        // Instantiate a bullet at the player position taking into account offset
        // and rotating the bullet based on the player's rotation.
        Bullet blt = Instantiate(m_bulletPrefab,
            m_bulletFirePoint.position,
            Quaternion.Euler(new Vector3(0f, 0f, transform.localEulerAngles.z + m_bulletRotationZOffset)));

        // Assign the bullet's owner to be this gameobject (the player)
        blt.Owner = tag;

        return blt;
    }
    #endregion

    #region Member variables
    [SerializeField]
    private Bullet m_bulletPrefab;

    [SerializeField]
    [Tooltip("The point from where the bullet will be fired")]
    private Transform m_bulletFirePoint = null;

    [SerializeField]
    [Tooltip("The offset in the z axis to adjust the bullet rotation.")]
    private float m_bulletRotationZOffset = 0f;

    [SerializeField]
    private float m_bulletSpeed = 2f;
    #endregion
}
