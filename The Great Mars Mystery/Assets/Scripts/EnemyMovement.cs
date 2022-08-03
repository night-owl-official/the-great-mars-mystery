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
    [SerializeField]
    private float m_walkSpeed = 0.6f;

    [SerializeField]
    [Range(0.01f, 0.1f)]
    private float m_rotationSmoothing = 0.02f;

    private Rigidbody2D m_rb = null;
    Vector2 m_movementDirection = Vector2.zero;
    float m_rbRotationLastFrame = 0f;
    #endregion
}
