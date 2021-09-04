using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public GameObject prefab_text;

    public Slider Healthbar;
    public float maxHealth = 3;
    private float currHealth;

    public float Diffence = 0;
    
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        currHealth = maxHealth;
        Healthbar.value = 1.0f;
    }

    public float getHealth()
    {
        return currHealth;
    }

    public void TakeDamage(float damage)
    {
        currHealth -= (damage - Diffence); 
        
        StartCoroutine(CombatText((int)(damage - Diffence)));
        editHealthbar();

        if (currHealth <= 0) 
        { 
            Death();
        }
        else
        {
            anim.SetTrigger("gotHit");
        }

    }

    public void editHealthbar()
    {
        float percHealth = Util.NumToPerc(currHealth, maxHealth);
        Healthbar.value = percHealth;
    }

    IEnumerator CombatText(int dam)
    {
        GameObject tmpObj = Instantiate(prefab_text, transform.position, Quaternion.identity);
        tmpObj.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = dam.ToString();
        tmpObj.GetComponent<Rigidbody2D>().velocity = Vector2.up * 3 + ((Vector2.right * 1.5f) * Util.randomLeftOrRight());

        yield return new WaitForSeconds(0.5f);
        Destroy(tmpObj.gameObject);
    }

    public void Death()
    {
        anim.SetBool("Death", true);

        remove_RB_BC();
        Destroy(gameObject, 2);

        try
        {
            GetComponent<EnemyAttack>().enabled = false;
            GetComponent<EnemyPatrol>().enabled = false;
        }catch{ }


        EventHandler.curr.TriggerOnEnemyDeath();
    }

    public void remove_RB_BC()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<BoxCollider2D>().enabled = false;   
    }

}
