using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Health buildHealth;
    DamageDealer damageDealer;
    GameController gamecontroller;

    void Awake()
    {
        damageDealer = GetComponent<DamageDealer>();
        gamecontroller = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            buildHealth = other.GetComponent<Health>();
            if (buildHealth != null)
            {
                if (gamecontroller.isTank)
                    buildHealth.TakeDamage(damageDealer.GetTankDamage());
                else if (gamecontroller.isStriker)
                    buildHealth.TakeDamage(damageDealer.GetStrikerDamage());
            }
        }
        Destroy(gameObject);
    }
}




