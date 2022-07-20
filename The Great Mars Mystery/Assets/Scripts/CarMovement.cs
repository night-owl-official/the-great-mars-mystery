using UnityEngine;
/*The sprites pivot point will dictate how the car appears to steer 
 * remember to set the point near the back of the vehicle */


// Make sure the Car movement has access to Rigidbody and Collider
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class CarMovement : MonoBehaviour
{
    #region Methods
    // Called once at the start
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Called once every frame
    void Update()
    {
        // Retrieve the user's input with a value between -1 and 1
        m_horizontal = Input.GetAxisRaw("Horizontal");
        m_vertical = Input.GetAxisRaw("Vertical");
    }

    // Called once every Physics update
    void FixedUpdate()
    {
        /*
        
        we assign the speed amount to the up and down controls
        we calculate the dot product  

         */
        //we assign the direction of rotation to the left and right controls
        //this is the rotation around the vertical axis(pivot point) or YAW of the vehicle
        m_yaw = -m_horizontal;
        //we assign the speed control to the up and down controls
        m_speed = m_vertical * m_acceleration;
        //The dot product of the velocity and the relitive direction of up is calculated,
        //if the two vectors are the same direction the product is positive, and if they are oposite
        //the value is negative.
        //Mathf.Sign then gives us a positive 1 or -1 relative to the calculated product
        //this allows us to avoid using if statement to select direction of rotation
        m_direction = Mathf.Sign(Vector2.Dot(m_rb.velocity, m_rb.GetRelativeVector(Vector2.up)));
        //set the amount of rotation to be added when a force is applied
        m_rb.rotation += m_yaw * m_yawRate * m_rb.velocity.magnitude * m_direction;

        //adds the force to the vehicle in the forward or backward direction 
        //related to the up or down arrow
        m_rb.AddRelativeForce(Vector2.up * m_speed);

        //adds a small force in the left and right direction to compensate for some vehicle drift
        //on button release
        m_rb.AddRelativeForce(-Vector2.right * m_rb.velocity.magnitude * m_yaw / 2);
    }
    #endregion

    #region Member variables
    [SerializeField]
    private float m_acceleration = 5f;
    [SerializeField]
    private float m_yawRate = 5f;

    private Rigidbody2D m_rb;

    private float m_horizontal;
    private float m_vertical;
    private float m_yaw;
    private float m_speed;
    private float m_direction;
    #endregion
}
