using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health {
    #region Methods
    // Start is called before the first frame update
    protected override void Start() {
        healthBarUI = GameObject.FindWithTag("PlayerHealthUI").GetComponent<Slider>();
        m_currentHP -= damageTaken;
        SetPlayerHealthBarUI(m_currentHP);
    }

    public override void DeductHPs(float amount) {
        base.DeductHPs(amount);
        damageTaken += amount;
        SetPlayerHealthBarUI(m_currentHP);
    }

    public static void Reset() {
        damageTaken = 0f;
    }

    private static void SetPlayerHealthBarUI(float value) {
        if (!healthBarUI)
            return;

        healthBarUI.value = value / 100f;
    }
    #endregion

    #region Member variables
    private static Slider healthBarUI;
    private static float damageTaken = 0f; // Used to keep the health the same across scenes
    #endregion
}
