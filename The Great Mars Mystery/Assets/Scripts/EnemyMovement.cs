using UnityEngine;

// Make sure we have access to Rigidbody and Collider
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class EnemyMovement : MonoBehaviour {

    #region Methods
    // Called once at the start
    private void Start() {
        m_rb = GetComponent<Rigidbody2D>();

        // At the start the rotations are the same
        m_rbRotationLastFrame = m_rb.rotation;
    }

    /// <summary>
    /// Sets the movement direction.
    /// </summary>
    /// <param name="destination">The destination for the enemy to reach.</param>
    public void UpdateMovementDirection(Vector2 destination) {
        m_movementDirection = destination - m_rb.position;
    }

    /// <summary>
    /// Checks whether the target is in the enemy's line of sight
    /// unobstructed by obstacles.
    /// </summary>
    /// <param name="targetMask">The layer mask used for the target physics layer.</param>
    /// <returns>True if the target is in sight, false otherwise.</returns>
    public bool CheckForTargetInLineOfSight(LayerMask targetMask) {
        // Find the target if it's present within the given enemy's view radius
        Collider2D targetCollider =
            Physics2D.OverlapCircle(transform.position, m_viewRadius, targetMask);

        // Target is within the view radius
        if (targetCollider) {
            // Vector from enemy position to the target's position
            Vector2 directionToTarget =
                (targetCollider.transform.position - transform.position).normalized;

            // Calculate the angle between the enemy's "forward" vector
            // and the target's positional vector
            float angle =
                Vector2.Angle(m_isForwardUp ? transform.up : transform.right, directionToTarget);

            // Check if the angle is within the enemy's view cone
            if (angle < m_viewConeAngle / 2f) {
                // When the target is in the view cone but hidden
                // behind an obstacle then it's as if the enemy doesn't see
                // the target, it does see it otherwise
                return !CheckForObstaclesInLineOfSight(targetCollider.transform);
            }
        }

        // Target's not in the view radius
        // nor is it in the view cone
        return false;
    }

    /// <summary>
    /// Uses raycasting to check for obstacle in the way.
    /// </summary>
    /// <param name="target">The enemy's target that could be in the view cone.</param>
    /// <returns>True if there's an obstacle in the way, false otherwise.</returns>
    public bool CheckForObstaclesInLineOfSight(Transform target = null) {
        float rayDistance = m_lineOfSightRange;
        Vector2 rayDirection = m_isForwardUp ? transform.up : transform.right;

        // When the target parameter is set
        // we want the ray to reach the target.
        if (target) {
            rayDirection = (target.position - transform.position).normalized;
            rayDistance = Vector2.Distance(transform.position, target.position);
        }

        // Cast a ray in the enemy's forward direction, either y or x axis,
        // that goes as far as the given line of sight range or the target's position.
        RaycastHit2D hit =
            Physics2D.Raycast(transform.position,
                rayDirection,
                rayDistance,
                m_lineOfSightBlockerMask);

        // When debug is checked, visualize line of sight
        if (m_visualizeLineOfSight)
            Debug.DrawRay(transform.position,
                m_isForwardUp ? transform.up : transform.right *
                rayDistance,
                Color.green);

        // Obstacle was hit
        if (hit.collider)
            return true;

        // Nothing was hit
        return false;
    }

    /// <summary>
    /// Tells the rigidbody to move in the given direction
    /// at the desired speed.
    /// </summary>
    /// <param name="runMultiplier">A multiplier that increases the movement speed.</param>
    public void MoveToDestination(float runMultiplier = 1f) {
        m_rb.MovePosition(m_rb.position + m_movementDirection.normalized *
                m_walkSpeed * Time.deltaTime * runMultiplier);
    }

    /// <summary>
    /// Calculates the rotation angle and applies it to the rigidbody.
    /// </summary>
    /// <param name="runMultiplier">A multiplier that increases the rotation speed.</param>
    public void RotateToFaceMoveDirection(float runMultiplier = 1f) {
        // Update last frame rotation
        m_rbRotationLastFrame = m_rb.rotation;

        // Find the angle between the horizontal axis and the vector
        // pointing in the direction the enemy should face
        // The angle is then converted into degrees
        // to make sure it's perfectly aligned with the mouse position
        float lookAngle =
            Mathf.Atan2(m_movementDirection.y, m_movementDirection.x) * Mathf.Rad2Deg;

        // Apply some smoothing when rotating the character
        m_rb.rotation =
            Mathf.LerpAngle(m_rb.rotation, lookAngle, m_rotationSmoothing * runMultiplier);
    }

    /// <summary>
    /// Checks whether the enemy is still rotating.
    /// </summary>
    /// <returns>True if the enemy is still rotating, false otherwise.</returns>
    public bool IsStillRotating() {
        // Enemy is still rotating when the rotation last frame
        // is different from the rotation this frame
        return !Mathf.Approximately(m_rbRotationLastFrame, m_rb.rotation);
    }

    /// <summary>
    /// Calculates the distance from the enemy's rigidbody to the given destination
    /// </summary>
    /// <param name="destination">The destination the enemy is trying to reach.</param>
    /// <returns>The distance between the enemy and the destination.</returns>
    public float DistanceFromDestination(Vector2 destination) {
        return Vector2.Distance(destination, m_rb.position);
    }
    #endregion

    #region Member variables
    [Header("Movement")]

    [SerializeField]
    private float m_walkSpeed = 0.6f;

    [SerializeField]
    [Range(0.01f, 0.1f)]
    private float m_rotationSmoothing = 0.02f;

    [SerializeField]
    [Tooltip("Is the enemy's forward vector the y axis?")]
    private bool m_isForwardUp = true;

    [Header("Sight")]
    [Space]

    [SerializeField]
    private float m_viewRadius = 3f;

    [SerializeField]
    [Tooltip("The view cone representing the enemy's FOV. In degrees")]
    [Range(0f, 360f)]
    private float m_viewConeAngle = 90f;

    [SerializeField]
    [Tooltip("How far the enemy can see ahead")]
    private float m_lineOfSightRange = 1.5f;

    [SerializeField]
    [Tooltip("Physics layers that can intercept the line of sight")]
    private LayerMask m_lineOfSightBlockerMask;

    [Header("Debug")]

    [SerializeField]
    private bool m_visualizeLineOfSight = false;

    private Rigidbody2D m_rb = null;
    Vector2 m_movementDirection = Vector2.zero;
    float m_rbRotationLastFrame = 0f;
    #endregion
}
