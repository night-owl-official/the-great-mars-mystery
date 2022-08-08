using UnityEngine;

// Make sure the agent chasing has access to enemy movement and attacking component
[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyAttacking))]
public class AgentChasing : MonoBehaviour {

    #region Methods
    // Start is called before the first frame update
    private void Start() {
        m_enemyMovement = GetComponent<EnemyMovement>();
        m_enemyAttacking = GetComponent<EnemyAttacking>();
    }

    // Update is called once per frame
    private void Update() {
        HandleChaseAndAttackLogic();
    }

    // Getter for chasing flag
    public bool HasEngagedTarget() { return m_isChasing; }

    /// <summary>
    /// Rotates to face target and advances this agent towards it.
    /// </summary>
    public void InitiateTargetEngagement() {
        // Need a target and access to enemy movement and attacking component
        if (!m_enemyMovement || !m_enemyAttacking || !m_target)
            return;

        // Face the target, double the speed for chases
        m_enemyMovement.RotateToFaceMoveDirection(2f);

        // Move towards the target, double the speed for chases
        // only when not attacking
        if (!m_enemyAttacking.IsAttacking)
            m_enemyMovement.MoveToDestination(2f);
    }

    /// <summary>
    /// Handles the logic for chasing and attacking.
    /// </summary>
    private void HandleChaseAndAttackLogic() {
        // Need access to enemy movement, attacking and a target
        if (!m_enemyMovement || !m_enemyAttacking || !m_target)
            return;

        // Get the distance to the target
        float distToTarget =
            m_enemyMovement.DistanceFromDestination(m_target.transform.position);

        // When the distance to the target is less than the attacking threshold
        // flag the attack, unflag it otherwise
        if (m_enemyAttacking.IsInAttackingRange(distToTarget)) {
            m_enemyAttacking.IsAttacking = true;

            // Update the movement direction
            m_enemyMovement.UpdateMovementDirection(m_target.transform.position);

            m_enemyAttacking.InitiateAttack();
        } else {
            m_enemyAttacking.IsAttacking = false;
        }

        // When the distance to the target is less than the chase threshold
        // flag the chase, unflag it otherwise
        if (distToTarget < m_chaseThreshold) {
            m_isChasing = true;

            // Update the movement direction
            m_enemyMovement.UpdateMovementDirection(m_target.transform.position);
        } else {
            m_isChasing = false;
        }
    }
    #endregion

    #region Member variables
    [SerializeField]
    private GameObject m_target = null;

    [SerializeField]
    [Tooltip("How far from the target to initiate chase. In meters")]
    private float m_chaseThreshold = 2.5f;

    private EnemyMovement m_enemyMovement = null;
    private EnemyAttacking m_enemyAttacking = null;
    private bool m_isChasing = false;
    #endregion
}

