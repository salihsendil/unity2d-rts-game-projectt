using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 20f;
    float projectileLifetime = 1f;
    float tankFireRate = 1.5f;
    float strikerFireRate = 0.5f;
    GameController gamecontroller;
    Coroutine firingCoroutine;
    private void Awake()
    {
        gamecontroller = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Enemy"))
        {
            Fire();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        StopCoroutine(firingCoroutine);
    }
    void Fire()
    {
        if (gamecontroller.isTank)
            firingCoroutine = StartCoroutine(FireContinously(tankFireRate));
        else if (gamecontroller.isStriker)
            firingCoroutine = StartCoroutine(FireContinously(strikerFireRate));
    }

    IEnumerator FireContinously(float fireRate)
    {
        while (true)
        {
            GameObject instance = Instantiate(projectilePrefab,
                                          gamecontroller.selectedObj.position,
                                          gamecontroller.selectedObj.rotation);
            Rigidbody2D myrb = instance.GetComponent<Rigidbody2D>();
            if (myrb != null)
            {
                myrb.velocity = myrb.transform.up * projectileSpeed;
            }
            Destroy(instance, projectileLifetime);
            yield return new WaitForSeconds(fireRate);
        }
    }
}

