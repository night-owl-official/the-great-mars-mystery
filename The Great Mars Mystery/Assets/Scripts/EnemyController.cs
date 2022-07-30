using UnityEngine;

// Make sure we have access to AgentPatrolling
[RequireComponent(typeof(AgentPatrolling))]
public class EnemyController : MonoBehaviour {

    #region Methods
    // Called once at the start
    private void Start() {
        m_agentPatrolling = GetComponent<AgentPatrolling>();
    }
    #endregion

    #region Member variables
    private AgentPatrolling m_agentPatrolling = null;
    #endregion
}
