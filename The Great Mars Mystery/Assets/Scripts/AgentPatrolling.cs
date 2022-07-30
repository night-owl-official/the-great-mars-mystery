using System.Collections;
using UnityEngine;

public class AgentPatrolling : MonoBehaviour {
    #region Methods
    // Called once at the start of the game
    private void Start() {
        m_rb = GetComponent<Rigidbody2D>();

        // At the start the rotations are the same
        m_rbRotationLastFrame = m_rb.rotation;

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

        // Get the movement direction from the current position to the next waypoint
        Vector2 faceDirection =
            m_waypoints[m_currentWaypoint] - m_rb.position;
        // Calculate the facing rotation angle
        float lookAngle = Mathf.Atan2(faceDirection.y, faceDirection.x) * Mathf.Rad2Deg;

        // Rotate to face the next waypoint
        // Loop until the agent is done rotating
        do {
            // Update last frame rotation
            m_rbRotationLastFrame = m_rb.rotation;

            // Apply some smoothing when rotating the character
            m_rb.rotation = Mathf.LerpAngle(m_rb.rotation, lookAngle, m_turnSmoothing);

            // Wait until next frame to run the loop again
            yield return null;
        } while (!Mathf.Approximately(m_rb.rotation, m_rbRotationLastFrame));

        // Move to the next waypoint
        // Loop until our position reaches the desired position
        while (!IsSwitchingWaypoints()) {
            m_rb.MovePosition(m_rb.position + faceDirection.normalized *
                m_walkSpeed * Time.deltaTime);

            // Wait until next frame to run the loop again
            yield return null;
        }
    }

    /// <summary>
    /// Switches from the current waypoint to the next when the current waypoint is reached.
    /// </summary>
    /// <returns>True if there's a new waypoint set, false otherwise.</returns>
    private bool IsSwitchingWaypoints() {
        float dist =
            Vector2.Distance(m_waypoints[m_currentWaypoint], m_rb.position);

        bool isDistanceLessThanStoppingDistance = dist < m_stoppingDistance;

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
    private float m_walkSpeed = 0.6f;

    [SerializeField]
    private float m_turnSmoothing = 0.02f;

    [SerializeField]
    [Tooltip("How far this AI will stop from the waypoint")]
    private float m_stoppingDistance = 0.2f;

    [SerializeField]
    private Vector2[] m_waypoints;

    private int m_currentWaypoint = 0;
    private bool m_isPatrolling = true;
    private float m_rbRotationLastFrame = 0f;
    private Rigidbody2D m_rb;
    #endregion
}