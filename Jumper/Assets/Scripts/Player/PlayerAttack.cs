using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public LayerMask EnemyLayer;
    public Transform hitLocation;
    
    public float hitRange = 2f;
    public float AttackDamage = 1f;
    public float AttackRate = 0.5f;
    bool canAttack = true;
    
    Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canAttack)
            StartCoroutine(AttackDelay());

    }
    IEnumerator AttackDelay()
    {
        canAttack = false;
        Attack();
        yield return new WaitForSeconds(AttackRate);
        canAttack = true;
    }

    void Attack()
    {
        anim.SetTrigger("Attacking");
        
        Collider2D[] hit_enemies = Physics2D.OverlapCircleAll(hitLocation.position, hitRange, EnemyLayer);
        DamageEnemies(hit_enemies);
    }

    private void DamageEnemies(Collider2D[] hit_enemies)
    {
        string lastHit = "";
        foreach (Collider2D e in hit_enemies)
        {
            if (lastHit != e.name)
            {
                lastHit = e.name;
                e.GetComponent<EnemyHealth>().TakeDamage(AttackDamage);
            }
            else
                break;
        }
    }
}
