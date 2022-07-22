using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    #region Methods
    // Called once per frame
    private void Update() {
        // Shoot a bullet when the user presses the left mouse button
        if (Input.GetMouseButtonDown(0)) {
            Bullet bullet = SpawnBullet();
        }
    }

    /// <summary>
    /// Instantiates a bullet
    /// </summary>
    /// <returns>The spawned bullet</returns>
    private Bullet SpawnBullet() {
        // Instantiate a bullet at the player position taking into account offset
        // and maintaining the original rotation of the bullet prefab
        Bullet blt = Instantiate(m_bulletPrefab,
            (Vector2)transform.position + m_bulletSpawnPoint,
            m_bulletPrefab.transform.rotation);

        // Assign the bullet's owner to be this gameobject (the player)
        blt.Owner = tag;

        return blt;
    }
    #endregion

    #region Member variables
    [SerializeField]
    private Bullet m_bulletPrefab;

    [SerializeField]
    [Tooltip("The offset from the player where the bullet will spawn")]
    private Vector2 m_bulletSpawnPoint = new Vector2(0.015f, 0.2f);
    #endregion
}
