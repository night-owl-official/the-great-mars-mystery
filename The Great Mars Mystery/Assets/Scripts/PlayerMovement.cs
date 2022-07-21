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
    }
    #endregion

    #region Member variables
    [SerializeField]
    private float m_speed = 1f;

    private Rigidbody2D m_rb;

    private float m_hInput;
    private float m_vInput;
    #endregion
}
