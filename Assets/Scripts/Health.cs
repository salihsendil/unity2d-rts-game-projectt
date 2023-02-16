using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int buildHealth = 500;
    [SerializeField] int tankHealth = 900;
    [SerializeField] int strikerHealth = 100;

    public void TakeDamage(int damage)
    {
        buildHealth -= damage;
        if (buildHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public int GetBuildHealth()
    {
        return buildHealth;
    }
    public int GetTankHealth()
    {
        return tankHealth;
    }
    public int GetStrikerHealth()
    {
        return strikerHealth;
    }
}
