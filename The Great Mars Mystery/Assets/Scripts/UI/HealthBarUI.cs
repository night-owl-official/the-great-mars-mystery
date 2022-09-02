using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Slider slider;
    public Rigidbody2D enemy;
    public GameObject healthBar;
    // Start is called before the first frame update
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }

    public void Update()
    {
        if(enemy != null)
        {
            Vector3 enemPos = enemy.transform.position;
            enemPos.y = enemPos.y + 0.20f;
            slider.transform.position = enemPos;

            if(slider.value <= 0)
            {
                Destroy(healthBar);
            }
        }

    }
}
