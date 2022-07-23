using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;
    
    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health<=0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log(gameObject.name+" Died");
        Destroy(gameObject);
    }

}
