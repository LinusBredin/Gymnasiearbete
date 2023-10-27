using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    float enemyHealth;
    public float enemyMaxHealth = 2f;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = enemyMaxHealth; 
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHealth <= 0)
        {
            Destroy(enemy);
        }
    }

    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
        Debug.Log("Damage Taken");
    }
}
