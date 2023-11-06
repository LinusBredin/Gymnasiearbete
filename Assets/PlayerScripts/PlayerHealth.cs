using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerHealth : MonoBehaviour
{

    float currentHealth;
    public float maxHealth = 5;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("Game Over");
        }
    }

    public void PlayerTakesDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took damage");
    }
}
