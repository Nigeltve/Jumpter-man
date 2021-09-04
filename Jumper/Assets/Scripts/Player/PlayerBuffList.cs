using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerBuffList : MonoBehaviour
{
    GameObject spellEffectsHandler;
    Animator anim;

    private void Start()
    {
        spellEffectsHandler = GameObject.FindGameObjectWithTag("SpellEffect");
        anim = spellEffectsHandler.GetComponent<Animator>();
    }

    void playBuffAnimation(int id)
    {
        switch (id)
        {
            case 1: //buff speed
                anim.SetTrigger("Fire1");
                break;
            case 2: //buff Damage
                anim.SetTrigger("Speed");
                break;

            default:
                break;
        }
    }


    public IEnumerator BuffSpeed(GameObject go, float amount, float duration)
    {
        EventHandler.curr.TriggerAddSpeedBuff(duration);
        GetComponent<PlayerMovement>().runSpeed += amount;

        go.GetComponent<BoxCollider2D>().enabled = false;
        go.GetComponent<SpriteRenderer>().enabled = false;

        playBuffAnimation(2);

        yield return new WaitForSeconds(duration);
        GetComponent<PlayerMovement>().runSpeed -= amount;
        Destroy(go);
    }

    public IEnumerator BuffDamage(GameObject go, float amount, float duration)
    {
        EventHandler.curr.TriggerAddPowerBuff(duration);
        GetComponent<PlayerAttack>().AttackDamage += amount;
        go.GetComponent<BoxCollider2D>().enabled = false;
        go.GetComponent<SpriteRenderer>().enabled = false;

        playBuffAnimation(1);

        yield return new WaitForSeconds(duration);
        GetComponent<PlayerAttack>().AttackDamage -= amount;
        Destroy(go);
    }
}
