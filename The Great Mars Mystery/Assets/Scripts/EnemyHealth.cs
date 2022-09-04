using UnityEngine.UI;
using UnityEngine;

public class EnemyHealth : Health {
    #region Methods
    // Start is called before the first frame update
    protected override void Start() {
        // Set the current health to max
        m_currentHP = m_maxHP;

        enemyHPBar = GetComponentInChildren<Slider>();
        enemyHPBar.maxValue = m_maxHP;
        enemyHPBar.value = m_currentHP;
    }

    private void Update() {
        enemyHPBar.transform.LookAt(Camera.main.transform);
    }

    public override void DeductHPs(float amount) {
        base.DeductHPs(amount);
        enemyHPBar.value = m_currentHP;
    }
    #endregion

    #region Member variables
    private Slider enemyHPBar;
    #endregion
}
