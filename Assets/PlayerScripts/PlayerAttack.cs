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
    public Animator animator;

    private RaycastHit2D[] hits;
    private void Update()
    {
        animator.SetBool("Attack?", false);

        if (timeBetweenAttacks <= 0)
        {
            if(UserInput.instance.playerControls.Player.Attack.WasPerformedThisFrame())
            {
                animator.SetBool("Attack?", true);

                Debug.Log("Damage Initiated");
                hits = Physics2D.CircleCastAll(attackPos.position , attackRange , transform.right, 0f, whatIsEnemies);
                for(int i = 0; i < hits.Length; i++)
                {
                    IDamage Idamage = hits[i].collider.gameObject.GetComponent<IDamage>();

                    if (Idamage != null)
                    {
                        Idamage.Damage(damage);
                    }
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
