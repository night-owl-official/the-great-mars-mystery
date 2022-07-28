using UnityEngine;

// Make sure the Player movement has access to Rigidbody and Collider
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class AIPatrol : MonoBehaviour {
    #region Methods
    // Called once at the start of the game
    private void Start() {
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Called once per frame
    private void Update() {
        // Go to the next waypoint when the current one was visited
        if (m_isPatrolling)
            SwitchWaypoint();
    }

    // Called once every physics update
    private void FixedUpdate() {
        if (m_isPatrolling) {
            // Get the movement direction from the current position to the next waypoint
            Vector2 direction =
                m_waypoints[m_currentWaypoint] - m_rb.position;
            // Calculate the facing rotation angle
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            m_rb.MovePosition(m_rb.position +
                (m_walkSpeed * Time.deltaTime * direction.normalized));
            m_rb.MoveRotation(angle);
        }
    }

    /// <summary>
    /// Switches from the current waypoint to the next when the current waypoint is reached.
    /// </summary>
    private void SwitchWaypoint() {
        float dist =
            Vector2.Distance(m_waypoints[m_currentWaypoint], m_rb.position);

        // Switch to the next waypoint after the current one has been explored
        if (dist < m_stoppingDistance)
            // Move onto the next waypoint in the list and loop back around
            // to the first waypoint when the last is reached
            m_currentWaypoint = (m_currentWaypoint + 1) % m_waypoints.Length;
    }
    #endregion

    #region Memeber variables
    [SerializeField]
    private float m_walkSpeed = 1f;

    [SerializeField]
    [Tooltip("How far this AI will stop from the waypoint")]
    private float m_stoppingDistance = 0.2f;

    [SerializeField]
    private Vector2[] m_waypoints;

    private int m_currentWaypoint = 0;
    private bool m_isPatrolling = true;
    private Rigidbody2D m_rb;
    #endregion
}