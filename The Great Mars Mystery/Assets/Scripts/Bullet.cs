﻿using UnityEngine;

// Make sure the Player movement has access to Rigidbody and Collider
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Bullet : MonoBehaviour {

    #region Methods
    // Getter and Setter for the property Owner
    public string Owner {
        get { return m_owner; }
        set {
            if (m_owner == "Untagged")
                m_owner = value;
            else
                return;
        }
    }

    // Runs when the collider enters a trigger collision
    private void OnTriggerEnter2D(Collider2D collision) {
        // Bullet is destroyed unless there's a collision
        // with the entity who shot the bullet
        if (!collision.CompareTag(m_owner))
            Destroy(gameObject);
    }

    // Called when no camera (including the scene camera) sees this object
    private void OnBecameInvisible() {
        // Bullets out of bounds get destroyed
        Destroy(gameObject);
    }
    #endregion

    #region Member variables
    // Tag of whoever shot the bullet
    private string m_owner = "Untagged";
    #endregion
}
