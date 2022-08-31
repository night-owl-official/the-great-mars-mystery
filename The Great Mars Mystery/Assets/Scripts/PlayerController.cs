using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerShooting))]
[RequireComponent(typeof(Health))]
public class PlayerController : MonoBehaviour {

    #region Methods
    // Start is called before the first frame update
    private void Start() {
        m_playerMovement = GetComponent<PlayerMovement>();
        m_playerShooting = GetComponent<PlayerShooting>();
        m_playerHealth = GetComponent<Health>();
    }

    // Update is called once per frame
    private void Update() {
        // Destroy the gameobject upon death
        if (m_playerHealth.IsZero()){
            FindObjectOfType<GameManager>().killScreen();
            Destroy(gameObject);
        }

        SetMovementInputs();

        m_playerShooting.InitiateShooting(Input.GetButtonDown("Fire1"));
    }

    // FixedUpdate is called once per physics update
    private void FixedUpdate() {
        m_playerMovement.InitiateMovement(m_hInput, m_vInput, m_worldMousePos);
    }

    /// <summary>
    /// Sets the horizontal and vertical user inputs
    /// as well as the current mouse position in world coordinates.
    /// </summary>
    private void SetMovementInputs() {
        // Retrieve the user's input with a value between -1 and 1
        m_hInput = Input.GetAxisRaw("Horizontal");
        m_vInput = Input.GetAxisRaw("Vertical");

        // Get the mouse position in world coordinates, ignore the z axis
        m_worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    #endregion

    #region Member variables
    private PlayerMovement m_playerMovement = null;
    private PlayerShooting m_playerShooting = null;
    private Health m_playerHealth = null;

    private float m_hInput = 0f;
    private float m_vInput = 0f;
    private Vector2 m_worldMousePos = Vector2.zero;
    #endregion
}
