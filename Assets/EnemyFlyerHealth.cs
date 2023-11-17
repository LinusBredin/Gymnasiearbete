using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFlyerHealth : MonoBehaviour, IDamage
{
    float enemyFlyerHealth;
    public float enemyFlyerMaxHealth = 2f;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemyFlyerHealth = enemyFlyerMaxHealth; 
    }

    public void Damage(float damageAmount)
    {
        enemyFlyerHealth -= damageAmount;

        if (enemyFlyerHealth <= 0)
        {
            Destroy(enemy);
        }
    }
}
