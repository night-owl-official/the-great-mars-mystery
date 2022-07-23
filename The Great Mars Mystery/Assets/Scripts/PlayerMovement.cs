using UnityEngine;

// Make sure the Player movement has access to Rigidbody and Collider
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour {

    #region Methods
    // Called once at the start
    private void Start() {
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Called once every frame
    private void Update() {
        // Retrieve the user's input with a value between -1 and 1
        m_hInput = Input.GetAxisRaw("Horizontal");
        m_vInput = Input.GetAxisRaw("Vertical");

        // Get the mouse position in world coordinates, ignore the z axis
        m_worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Called once every Physics update
    private void FixedUpdate() {
        // Add a velocity to the current rigidbody's position
        // The resulting vector gets normalized to avoid faster diagonal movement
        // and then multiplied by a speed variable to control the character's speed
        // and by delta time to make it frame rate independent.
        m_rb.MovePosition(m_rb.position +
            new Vector2(m_hInput, m_vInput).normalized * m_speed *
            Time.fixedDeltaTime);

        ApplyRotation();
    }

    /// <summary>
    /// Calculates the rotation angle and applies it to the rigidbody.
    /// </summary>
    private void ApplyRotation() {
        // Vector from the player to the mouse position
        Vector2 playerToMouse = m_worldMousePos - m_rb.position;
        // Find the angle between the horizontal axis and the vector
        // pointing in the direction where the mouse is
        // The angle is then converted into degrees and given an 90 degrees offset
        // to make sure it's perfectly aligned with the mouse position
        float lookAngle = Mathf.Atan2(playerToMouse.y, playerToMouse.x) * Mathf.Rad2Deg - 90f;

        // Apply some smoothing when rotating the character
        m_rb.rotation = Mathf.LerpAngle(m_rb.rotation, lookAngle, m_rotationSmoothing);
    }
    #endregion

    #region Member variables
    [SerializeField]
    private float m_speed = 1f;

    [SerializeField]
    [Range(0.05f, 0.5f)]
    private float m_rotationSmoothing = 0.2f;

    private Rigidbody2D m_rb;

    private float m_hInput;
    private float m_vInput;
    private Vector2 m_worldMousePos;
    #endregion
}
