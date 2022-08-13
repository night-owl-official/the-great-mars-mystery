﻿using System.Collections;
using UnityEngine;

// Make sure the agent patrolling has access to enemy movement component
[RequireComponent(typeof(EnemyMovement))]
public class AgentPatrolling : MonoBehaviour {

    #region Methods
    // Called once at the start
    private void Start() {
        m_enemyMovement = GetComponent<EnemyMovement>();
    }

    // Getter and Setter for patrol flag
    public bool IsPatrolling {
        get => m_isPatrolling;
        set => m_isPatrolling = value;
    }

    /// <summary>
    /// Starts the patrol coroutines.
    /// </summary>
    public void StartPatrol() {
        // Can't patrol without enemy movement
        if (!m_enemyMovement)
            return;

        // Start patrolling
        StartCoroutine(InitiatePatrol());
    }

    /// <summary>
    /// Coroutine used to keep the agent patrolling.
    /// </summary>
    private IEnumerator InitiatePatrol() {
        // Keep patrolling until told otherwise
        while (true) {
            // Stop coroutine when not in patrol
            if (!m_isPatrolling)
                yield break;

            // Start the coroutine once it has finished running
            yield return StartCoroutine(FaceAndMoveToNextWaypoint());
        }
    }

    /// <summary>
    /// Coroutine for the patrolling session.
    /// </summary>
    private IEnumerator FaceAndMoveToNextWaypoint() {
        // Update the movement direction
        m_enemyMovement.UpdateMovementDirection(m_waypoints[m_currentWaypoint]);

        // Rotate to face the next waypoint
        // Loop until the agent is done rotating
        do {
            // Stop coroutine when not in patrol
            if (!m_isPatrolling)
                yield break;

            m_enemyMovement.RotateToFaceMoveDirection();

            // Wait until next frame to run the loop again
            yield return null;
        } while (m_enemyMovement.IsStillRotating());

        // Move to the next waypoint
        // Loop until our position reaches the desired position
        while (!IsSwitchingWaypoints()) {
            // Stop coroutine when not in patrol
            if (!m_isPatrolling)
                yield break;

            m_enemyMovement.MoveToDestination();

            // Wait until next frame to run the loop again
            yield return null;
        }

        // Stop coroutine when not in patrol
        if (!m_isPatrolling)
            yield break;

        // Wait before facing the next waypoint
        yield return new WaitForSeconds(m_timeToWaitAtWaypoint);
    }

    /// <summary>
    /// Switches from the current waypoint to the next when the current waypoint is reached.
    /// </summary>
    /// <returns>True if there's a new waypoint set, false otherwise.</returns>
    private bool IsSwitchingWaypoints() {
        // Is our distance from the waypoint less than the stopping distance?
        bool hasReachedWaypoint =
            m_enemyMovement.DistanceFromDestination(m_waypoints[m_currentWaypoint]) <
            m_stoppingDistance;

        // Is there an obstacle in the way to the next waypoint?
        bool isObstacleInTheWay = m_enemyMovement.CheckForObstaclesInLineOfSight();

        // Switch to the next waypoint after the current one has been explored
        // or if there's an obstacle blocking the way
        if (hasReachedWaypoint || isObstacleInTheWay)
            // Move onto the next waypoint in the list and loop back around
            // to the first waypoint when the last is reached
            m_currentWaypoint = (m_currentWaypoint + 1) % m_waypoints.Length;

        return hasReachedWaypoint || isObstacleInTheWay;
    }
    #endregion

    #region Memeber variables
    [SerializeField]
    [Tooltip("How far this AI will stop from the waypoint")]
    private float m_stoppingDistance = 0.2f;

    [SerializeField]
    [Tooltip("Time, in seconds, to wait before moving to the next waypoint")]
    private float m_timeToWaitAtWaypoint = 2.5f;

    [SerializeField]
    private Vector2[] m_waypoints;

    private EnemyMovement m_enemyMovement = null;
    private int m_currentWaypoint = 0;
    private bool m_isPatrolling = false;
    #endregion
}