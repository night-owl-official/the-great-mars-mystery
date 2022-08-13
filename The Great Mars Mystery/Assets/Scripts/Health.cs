using UnityEngine;

public class Health : MonoBehaviour {

    #region Methods
    // Called before the first frame
    private void Start() {
        // Set the current health to max
        m_currentHP = m_maxHP;
    }

    /// <summary>
    /// Decreases the HPs by the given amount.
    /// </summary>
    /// <param name="amount">The amount to deduct from HPs.S</param>
    public void DeductHPs(float amount) {
        // Decrease current health, never underflowing zero
        m_currentHP = Mathf.Max(m_currentHP - amount, 0f);

        // Temporary log
        Debug.Log(name + " took " + amount + " damage. Current health: " + m_currentHP);
    }

    /// <summary>
    /// Checks to see if there's no HPs left.
    /// </summary>
    /// <returns>True if there are no more HPs left, false otherwise.</returns>
    public bool IsZero() { return m_currentHP <= 0f; }
    #endregion

    #region Member variables
    [SerializeField]
    private float m_maxHP = 100f;

    private float m_currentHP = 100f;
    #endregion
}
