using System.Collections;
using UnityEngine;

// Make sure the agent patrolling has access to enemy movement component
[RequireComponent(typeof(EnemyMovement))]
public class AgentPatrolling : MonoBehaviour {

    #region Methods
    // Called once at the start
    private void Start() {
        m_enemyMovement = GetComponent<EnemyMovement>();
    }

    // Called once every frame
    private void Update() {
        // Set the movement direction every frame
        // to make sure we always have the updated direction
        m_enemyMovement.SetMovementDirection(m_waypoints[m_currentWaypoint]);
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
        while (m_isPatrolling)
            // Start the coroutine once it has finished running
            yield return StartCoroutine(FaceAndMoveToNextWaypoint());
    }

    /// <summary>
    /// Coroutine for the patrolling session.
    /// </summary>
    private IEnumerator FaceAndMoveToNextWaypoint() {
        // Wait before facing the next waypoint
        yield return new WaitForSeconds(2.5f);

        // Rotate to face the next waypoint
        // Loop until the agent is done rotating
        while (m_enemyMovement.IsStillRotating()) {
            m_enemyMovement.RotateToFaceMoveDirection();

            // Wait until next frame to run the loop again
            yield return null;
        }

        // Move to the next waypoint
        // Loop until our position reaches the desired position
        while (!IsSwitchingWaypoints()) {
            m_enemyMovement.MoveToDestination();

            // Wait until next frame to run the loop again
            yield return null;
        }
    }

    /// <summary>
    /// Switches from the current waypoint to the next when the current waypoint is reached.
    /// </summary>
    /// <returns>True if there's a new waypoint set, false otherwise.</returns>
    private bool IsSwitchingWaypoints() {
        // Is our distance from the waypoint less than the stopping distance?
        bool isDistanceLessThanStoppingDistance =
            m_enemyMovement.DistanceFromDestination(m_waypoints[m_currentWaypoint]) <
            m_stoppingDistance;

        // Switch to the next waypoint after the current one has been explored
        if (isDistanceLessThanStoppingDistance)
            // Move onto the next waypoint in the list and loop back around
            // to the first waypoint when the last is reached
            m_currentWaypoint = (m_currentWaypoint + 1) % m_waypoints.Length;

        return isDistanceLessThanStoppingDistance;
    }
    #endregion

    #region Memeber variables
    [SerializeField]
    [Tooltip("How far this AI will stop from the waypoint")]
    private float m_stoppingDistance = 0.2f;

    [SerializeField]
    private Vector2[] m_waypoints;

    private EnemyMovement m_enemyMovement = null;
    private int m_currentWaypoint = 0;
    private bool m_isPatrolling = true;
    #endregion
}