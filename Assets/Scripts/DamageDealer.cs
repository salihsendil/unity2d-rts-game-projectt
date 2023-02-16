using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int strikerDamage = 5;
    public int tankDamage = 20;

    public int GetStrikerDamage()
    {
        return strikerDamage;
    }
    public int GetTankDamage()
    {
        return tankDamage;
    }
    public void Hit(GameObject obj)
    {
        Destroy(obj);
    }
}
