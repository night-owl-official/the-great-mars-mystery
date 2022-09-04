using UnityEngine.UI;
public class EnemyHealth : Health {
    #region Methods
    // Start is called before the first frame update
    protected override void Start() {
        // Set the current health to max
        m_currentHP = m_maxHP;
        enemyHPBar=gameObject.GetComponentInChildren<Slider>();
        enemyHPBar.maxValue=m_maxHP;
        enemyHPBar.value=m_currentHP;
    }

    public override void DeductHPs(float amount) {
        base.DeductHPs(amount);
        enemyHPBar.value=m_currentHP;
    }
    #endregion

    #region Member variables
    public Slider enemyHPBar;
    #endregion
}
