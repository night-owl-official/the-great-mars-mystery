using UnityEngine;

// Make sure we have access to Rigidbody and Collider
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class EnemyMovement : MonoBehaviour {

    #region Methods
    // Called once at the start
    private void Start() {
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Setter for movement direction
    public Vector2 MovementDirection {
        set { m_movementDirection = value; }
    }

    /// <summary>
    /// Tells the rigidbody to move in the given direction
    /// at the desired speed.
    /// </summary>
    public void MoveToDestination() {
        m_rb.MovePosition(m_rb.position + m_movementDirection.normalized *
                m_walkSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Calculates the rotation angle and applies it to the rigidbody.
    /// </summary>
    public void RotateToFaceMoveDirection() {
        // Find the angle between the horizontal axis and the vector
        // pointing in the direction the enemy should face
        // The angle is then converted into degrees
        // to make sure it's perfectly aligned with the mouse position
        float lookAngle =
            Mathf.Atan2(m_movementDirection.y, m_movementDirection.x) * Mathf.Rad2Deg;

        // Apply some smoothing when rotating the character
        m_rb.rotation = Mathf.LerpAngle(m_rb.rotation, lookAngle, m_rotationSmoothing);
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
    #endregion
}
