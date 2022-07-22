using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    #region Methods
    #endregion

    #region Member variables
    [SerializeField]
    private Bullet m_bulletPrefab;

    [SerializeField]
    [Tooltip("The offset from the player where the bullet will spawn")]
    private Vector2 m_bulletSpawnPoint;
    #endregion
}
