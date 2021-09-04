using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public GameObject prefab_text;

    public Slider Healthbar;
    public float maxHealth = 5f;
    public float currHealth;
    public float Diffence = 0.5f;
    
    Animator anim;

    private bool death = false;
    private void Start()
    {
        anim = GetComponent<Animator>();
        currHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currHealth -= (damage - Diffence);

        StartCoroutine(spawnObject((int)(damage - Diffence)));
        editHealthbar();

        if (currHealth <= 0)
        {
            Death();
        }
        else
        {
            anim.SetTrigger("TakeHit");
        }

    }

    public void editHealthbar()
    {
        float percHealth = Util.NumToPerc(currHealth, maxHealth);
        Healthbar.value = percHealth;
    }

    public void Death()
    {
        if (!death) {
            death = true;
            anim.SetTrigger("Death");
        }

        EventHandler.curr.TriggerOnPlayerDeath();
        GetComponent<PlayerMovement>().enabled = false;
        
    }

    IEnumerator spawnObject(int dam)
    {
        System.Random rng = new System.Random();

        int LorR = Util.randomLeftOrRight();

        GameObject tmpObj = Instantiate(prefab_text, transform.position, Quaternion.identity);
        tmpObj.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = dam.ToString();
        tmpObj.GetComponent<Rigidbody2D>().velocity = Vector2.up * 3 + ((Vector2.right * 1.5f) * LorR);

        yield return new WaitForSeconds(0.5f);
        Destroy(tmpObj.gameObject);
    }
}
