using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private float timeBetweenAttacks = 0;
    public float startTimeBetweenAttacks;



    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public int damage;

    private void Update()
    {
        if(timeBetweenAttacks <= 0)
        {
            if(UserInput.instance.playerControls.Player.Attack.WasPerformedThisFrame())
            {
                Debug.Log("Damage Initiated");
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position , attackRange , whatIsEnemies);
                for(int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage(damage);
                }
                timeBetweenAttacks = startTimeBetweenAttacks;
            }
        }
        else
        {
            timeBetweenAttacks -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
