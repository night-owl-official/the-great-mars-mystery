using System.Collections;
using UnityEngine;

public class EnemyAttacking : MonoBehaviour {

    #region Methods
    // Getter and setter for attacking flag
    public bool IsAttacking {
        get => m_isAttacking;
        set => m_isAttacking = value;
    }

    /// <summary>
    /// Figures out whether the enemy is in attacking range.
    /// </summary>
    /// <param name="distToTarget">The distance from the enemy to our target.</param>
    /// <returns>True if in attacking range, false otherwise.</returns>
    public bool IsInAttackingRange(float distToTarget) {
        return m_isMelee ? (distToTarget < m_hittingThreshold) : (distToTarget < m_firingThreshold);
    }

    /// <summary>
    /// Decides whether to launch a melee or ranged attack based on the type of enemy
    /// </summary>
    public void InitiateAttack() {
        // Don't execute if not in attack state
        if (!m_isAttacking)
            return;

        // Only attack when possible
        if (m_canAttack)
            StartCoroutine(PerformAttack());
    }

    private IEnumerator PerformAttack() {
        // Break out of the coroutine if not in attacking state
        if (!m_isAttacking)
            yield break;

        // Need access to a fire point when ranged
        if (!m_isMelee && !m_bulletFirePoint)
            yield break;

        // Need to wait for the given time before we can attack
        m_canAttack = false;

        // Choose ranged or melee based on the melee flag
        if (m_isMelee)
            Debug.Log("Hit Target");
        else
            FireBullet();

        // Random value between two and the max amount of bullets/hits per minute
        // to simulate a human's random behavior, then divide by 60 to convert
        // from per minute into per second
        float attacksPerSecond = Random.Range(2, m_isMelee ? m_hitRate : m_fireRate) / 60f;
        // Wait x amount of seconds based on fire rate
        yield return new WaitForSeconds(1f / attacksPerSecond);

        // Now we can attack again
        m_canAttack = true;
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
            Quaternion.Euler(new Vector3(0f, 0f, transform.localEulerAngles.z + m_bulletRotationZOffset)));

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

    [SerializeField]
    [Tooltip("How far from the target to start firing. In meters")]
    private float m_firingThreshold = 1.5f;

    [Header("Melee")]
    [Space]

    [SerializeField]
    [Tooltip("How many hits it can throw per minute")]
    private int m_hitRate = 300;

    [SerializeField]
    [Range(0.2f, 0.5f)]
    [Tooltip("How far from the target to start hitting. In meters")]
    private float m_hittingThreshold = 0.3f;

    private bool m_canAttack = true;
    private bool m_isAttacking = false;
    #endregion
}
