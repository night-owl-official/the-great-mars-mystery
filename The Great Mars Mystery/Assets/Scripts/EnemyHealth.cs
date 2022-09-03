public class EnemyHealth : Health {
    #region Methods
    // Start is called before the first frame update
    protected override void Start() {
        // Set the current health to max
        m_currentHP = m_maxHP;
    }
    #endregion

    #region Member variables
    #endregion
}
