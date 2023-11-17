using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargerHealth : MonoBehaviour, IDamage
{

    public float enemyChargerMaxHealth = 4;
    float enemyChargerCurrentHealth;
    public GameObject enemyCharger;

    // Start is called before the first frame update
    void Start()
    {
        enemyChargerCurrentHealth = enemyChargerMaxHealth;
    }

    // Update is called once per frame
    public void Damage(float damageAmount)
    {
         enemyChargerCurrentHealth -= damageAmount;

        if (enemyChargerCurrentHealth <= 0)
        {
            Destroy(enemyCharger);
        }
    }
}
