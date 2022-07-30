using UnityEngine;

// Enemy state machine
public enum EnemyState {
    None,
    Patrol,
    Chase,
    Attack,
    Die
}

// Make sure we have access to AgentPatrolling
[RequireComponent(typeof(AgentPatrolling))]
public class EnemyController : MonoBehaviour {

    #region Methods
    // Called once at the start
    private void Start() {
        m_agentPatrolling = GetComponent<AgentPatrolling>();
        m_currentState = EnemyState.Patrol;
    }

    // Called once every frame
    private void Update() {
        HandleStateMachine();
    }

    /// <summary>
    /// Checks the enemy state and handles
    /// whatever it's supposed to happen regarding that state.
    /// </summary>
    private void HandleStateMachine() {
        // Checks the current state and acts accordingly
        switch (m_currentState) {
            case EnemyState.Patrol:
                // Need to have a ref to agent patrolling
                if (!m_agentPatrolling)
                    break;

                // Initiate patrol state
                if (!m_isPatrolling) {
                    m_agentPatrolling.StartPatrol();
                    m_isPatrolling = true;
                }
                break;
            case EnemyState.Chase:
                // Stop patrolling
                m_isPatrolling = false;

                // To be inserted
                break;
            case EnemyState.Attack:
                // Stop patrolling
                m_isPatrolling = false;

                // To be inserted
                break;
            case EnemyState.Die:
                // Stop patrolling
                m_isPatrolling = false;

                // To be inserted
                break;
        }
    }
    #endregion

    #region Member variables
    private EnemyState m_currentState = EnemyState.None;
    private AgentPatrolling m_agentPatrolling = null;

    private bool m_isPatrolling = false;
    #endregion
}
