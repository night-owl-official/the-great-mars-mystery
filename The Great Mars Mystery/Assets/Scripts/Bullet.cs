using UnityEngine;

/*
 * It's important to set the bullet owner and damage
 * everytime a bullet is instantiated
 * for the shooter to avoid hitting themselves
 */

// Make sure the Player movement has access to Rigidbody and Collider
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Bullet : MonoBehaviour {

    #region Methods
    // Getter and Setter for the property Owner
    public string Owner {
        get { return m_owner; }
        set {
            // Owner can only be set once
            if (m_owner == "Untagged")
                m_owner = value;
        }
    }

    // Getter and Setter for damage
    public float Damage { 
        get => m_damage;
        set => m_damage = value;
    }

    // Runs when the collider enters a trigger collision
    private void OnTriggerEnter2D(Collider2D other) {
        // Upon collision with an entity that has a Health component
        // damage will be dealt to that entity
        DealDamage(other.GetComponent<Health>());

        // Bullet is destroyed unless there's a collision
        // with the entity who shot the bullet or if it's colliding with another bullet
        if (!other.CompareTag(m_owner) && !other.CompareTag(tag))
            Destroy(gameObject);
    }

    // Called when no camera (including the scene camera) sees this object
    private void OnBecameInvisible() {
        // Bullets out of bounds get destroyed
        Destroy(gameObject);
    }

    private void Start() {
        FindObjectOfType<SoundManager>().Play("shooting");
    }

    /// <summary>
    /// Deals damage to an entity.
    /// </summary>
    /// <param name="hp">The entity's health component.</param>
    private void DealDamage(Health hp) {
        if (!hp)
            return;

        hp.DeductHPs(m_damage);
    }
    #endregion

    #region Member variables
    // Tag of whoever shot the bullet
    private string m_owner = "Untagged";

    // How much damage a bullet will deal
    private float m_damage = 0f;
    #endregion
}
