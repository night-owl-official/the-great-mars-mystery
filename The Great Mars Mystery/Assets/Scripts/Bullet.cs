using UnityEngine;

// Make sure the Player movement has access to Rigidbody and Collider
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Bullet : MonoBehaviour {

    #region Methods
    // Runs when the collider enters a trigger collision
    private void OnTriggerEnter2D(Collider2D collision) {
        // Bullet is destroyed
        Destroy(gameObject);
    }

    // Called when no camera (including the scene camera) sees this object
    private void OnBecameInvisible() {
        // Bullets out of bounds get destroyed
        Destroy(gameObject);
    }
    #endregion

    #region Member variables
    #endregion
}
