using System.Collections;
using UnityEngine;

public class EnemyAttacking : MonoBehaviour {

    #region Methods
    /// <summary>
    /// Decides whether to launch a melee or ranged attack based on the type of enemy
    /// </summary>
    public void InitiateAttack() {
        if (m_isMelee)
            PerformMeleeAttack();
        else
            // Only fire if possible
            if (m_canFire)
                StartCoroutine(PerformRangedAttack());
    }

    private void PerformMeleeAttack() {
        // To be added
    }

    /// <summary>
    /// Spawns a bullet and fires it.
    /// </summary>
    private IEnumerator PerformRangedAttack() {
        // Need access to a fire point
        if (!m_bulletFirePoint)
            yield break;

        // Need to wait for the given time before we can fire the next bullet
        m_canFire = false;

        FireBullet();

        // Random value between one and the max amount of bullets per minute
        // to simulate a human being firing a weapon, then divide by 60 to convert
        // from per minute into per second
        float bulletsPerSecond = Random.Range(1, m_fireRate) / 60f;
        // Wait x amount of seconds based on fire rate
        yield return new WaitForSeconds(1f / bulletsPerSecond);

        // Now we can fire the next bullet
        m_canFire = true;
    }

    /// <summary>
    /// Spawns a bullet and fires it forward.
    /// </summary>
    private void FireBullet() {
        Bullet bullet = SpawnBullet();
        // Add a force to the bullet in the fire point up direction (forward)
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.AddForce(m_bulletFirePoint.up * m_bulletSpeed, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Instantiates a bullet
    /// </summary>
    /// <returns>The spawned bullet</returns>
    private Bullet SpawnBullet() {
        // Instantiate a bullet at the given fire point.
        Bullet blt = Instantiate(m_bulletPrefab,
            m_bulletFirePoint.position,
            Quaternion.Euler(new Vector3(0f, 0f, transform.rotation.z + m_bulletRotationZOffset)));

        // Assign the bullet's owner to be this gameobject (the enemy)
        blt.Owner = tag;

        return blt;
    }
    #endregion

    #region Member variables
    [SerializeField]
    private bool m_isMelee = false;

    [Header("Bullet Instantiation")]

    [Header("Ranged (Irrelevant when melee enemy)")]

    [SerializeField]
    private Bullet m_bulletPrefab = null;

    [SerializeField]
    [Tooltip("The point from where the bullet will be fired")]
    private Transform m_bulletFirePoint = null;

    [SerializeField]
    [Tooltip("The offset in the z axis to adjust the bullet rotation.")]
    private float m_bulletRotationZOffset = 0f;

    [Header("Fire Settings")]
    [Space]

    [SerializeField]
    private float m_bulletSpeed = 2f;

    [SerializeField]
    [Tooltip("How many bullets it can fire per minute")]
    private int m_fireRate = 180;

    private bool m_canFire = true;
    #endregion
}
