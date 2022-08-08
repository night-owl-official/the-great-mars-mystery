﻿using UnityEngine;

// Enemy state machine
public enum EnemyState {
    None,
    Patrol,
    ChaseAndAttack,
    Die
}

// Make sure we have access to AgentPatrolling, AgentChasing
[RequireComponent(typeof(AgentPatrolling))]
[RequireComponent(typeof(AgentChasing))]
public class EnemyController : MonoBehaviour {

    #region Methods
    // Called once at the start
    private void Start() {
        m_agentPatrolling = GetComponent<AgentPatrolling>();
        m_agentChasing = GetComponent<AgentChasing>();
    }

    // Called once every frame
    private void Update() {
        HandleStateSwitchingLogic();
        HandleStateMachine();
    }

    /// <summary>
    /// Switches the current enemy state based on certain conditions.
    /// </summary>
    private void HandleStateSwitchingLogic() {
        // Set the state to chasing when the agent
        // meets the conditions for a chase, patrol otherwise
        if (m_agentChasing.HasEngagedTarget())
            m_currentState = EnemyState.ChaseAndAttack;
        else
            m_currentState = EnemyState.Patrol;

        // Further logic to switch to different states will be added here
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
                if (!m_agentPatrolling.IsPatrolling) {
                    m_agentPatrolling.IsPatrolling = true;
                    m_agentPatrolling.StartPatrol();
                }
                break;
            case EnemyState.ChaseAndAttack:
                // Need to have a ref to agent chasing
                if (!m_agentChasing)
                    break;

                // Stop patrolling
                m_agentPatrolling.IsPatrolling = false;

                // Initiate chase and attack state
                m_agentChasing.InitiateTargetEngagement();
                break;
            case EnemyState.Die:
                // To be inserted
                break;
        }
    }
    #endregion

    #region Member variables
    private EnemyState m_currentState = EnemyState.None;
    private AgentPatrolling m_agentPatrolling = null;
    private AgentChasing m_agentChasing = null;
    #endregion
}