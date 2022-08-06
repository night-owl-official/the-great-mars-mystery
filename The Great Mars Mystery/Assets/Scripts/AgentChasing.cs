using UnityEngine;

// Make sure the agent chasing has access to enemy movement component
[RequireComponent(typeof(EnemyMovement))]
public class AgentChasing : MonoBehaviour {

    #region Methods
    // Start is called before the first frame update
    private void Start() {
        m_enemyMovement = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    private void Update() {
        // Need access to enemy movement and a target
        if (!m_enemyMovement || !m_target)
            return;

        // Get the distance to the target
        float distToTarget =
            m_enemyMovement.DistanceFromDestination(m_target.transform.position);

        // When the distance to the player is less than the chase threshold
        // flag the chase, unflag it otherwise
        if (distToTarget < m_chaseThreshold) {
            m_isChasing = true;

            // Update the movement direction
            m_enemyMovement.UpdateMovementDirection(m_target.transform.position);
        } else {
            m_isChasing = false;
        }
    }

    // Getter for chasing flag
    public bool IsChasing() { return m_isChasing; }

    /// <summary>
    /// Rotates to face target and advances this agent towards its target.
    /// </summary>
    public void InitiateChase() {
        // Need a target and access to enemy movement component
        if (!m_enemyMovement || !m_target)
            return;

        // Face the target, double the speed for chases
        m_enemyMovement.RotateToFaceMoveDirection(2f);

        // Move towards the target, double the speed for chases
        m_enemyMovement.MoveToDestination(2f);
    }
    #endregion

    #region Member variables
    [SerializeField]
    private GameObject m_target = null;

    [SerializeField]
    [Tooltip("How far from the target to initiate chase. In meters")]
    private float m_chaseThreshold = 1.5f;

    private EnemyMovement m_enemyMovement = null;
    private bool m_isChasing = false;
    #endregion
}

