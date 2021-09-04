using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public LayerMask PlayerLayer;
    public Transform hitLocation;

    public float attackSpeed = 1.0f;
    public float attackRange = 0.5f;
    public float attackDamage = 1.0f;
    private bool canAttack = true;

    Animator anim;
    Collider2D[] hit_enemies;
    private EnemyHealth Health;
    private void Start()
    {
        Health = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        hit_enemies = Physics2D.OverlapCircleAll(hitLocation.position, attackRange, PlayerLayer);
        if (hit_enemies.Length != 0 && canAttack && Health.getHealth() != 0)
        {
            StartCoroutine(AttakDelay());
        }
    }

    IEnumerator AttakDelay()
    {
        canAttack = false;
        AttackPlayer();

        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }

    void AttackPlayer()
    {
        anim.SetTrigger("Strike");

        //damage Player
        try {
            hit_enemies[0].GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
        catch
        {
            Debug.Log("No Player Found");
        }
    }


}
