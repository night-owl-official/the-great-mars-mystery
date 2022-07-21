using UnityEngine;
/*The sprites pivot point will dictate how the car appears to steer 
 * it's recommended to set it near the back of the vehicle */


// Make sure the Car movement has access to Rigidbody and Collider
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class CarMovement : MonoBehaviour {
    #region Methods
    // Called once at the start
    private void Start() {
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Called once every frame
    private void Update() {
        // Retrieve the user's input with a value between -1 and 1
        // Some input smoothing is preferred for vehicles

        // Invert the sideways input to match with the turn direction
        // e.g. right arrow = turn right
        m_hInput = Input.GetAxis("Horizontal") * -1;
        m_vInput = Input.GetAxis("Vertical");
    }

    // Called once every Physics update
    private void FixedUpdate() {
        ApplyEngineForce();
        ApplySteering();
    }

    /// <summary>
    /// Constructs the speed vector and uses it as the engine force
    /// propelling the car forward.
    /// </summary>
    private void ApplyEngineForce() {
        // Speed vector allows the car to move forward and backwards,
        // alongside the car's local y axis (forward in 2D), based on the user input
        m_speed = m_acceleration * m_vInput * transform.up;
        m_rb.AddForce(m_speed);
    }

    /// <summary>
    /// Figures out the direction of the rotation and applies it to the car.
    /// </summary>
    private void ApplySteering() {
        // The dot product allows us to figure out which way the car is going to turn
        // by calculating the angle between the velocity and the forward direction (up in 2D)
        // Mathf.Sign then gives us a positive 1 or -1 relative to the calculated product
        // which allows us to avoid using if statement to select direction of rotation
        m_turnDirection = Mathf.Sign(Vector2.Dot(m_rb.velocity, m_rb.GetRelativeVector(Vector2.up)));

        // Apply a rotation to the object based on its velocity and turn speed.
        // User input is also taken into account alongside the turn direction.
        m_rb.rotation += m_hInput * m_turnSpeed * m_rb.velocity.magnitude * m_turnDirection;
    }
    #endregion

    #region Member variables
    [SerializeField]
    private float m_acceleration = 5f;
    [SerializeField]
    private float m_turnSpeed = 5f;

    private Rigidbody2D m_rb;

    private float m_hInput;
    private float m_vInput;
    private Vector2 m_speed;
    private float m_turnDirection;
    #endregion
}
