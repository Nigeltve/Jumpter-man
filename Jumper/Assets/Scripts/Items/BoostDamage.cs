using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostDamage : MonoBehaviour
{
    public float duration = 4f;
    public float amount = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerBuffList PBL = collision.gameObject.GetComponent<PlayerBuffList>();
            StartCoroutine(PBL.BuffDamage(gameObject, amount, duration));
        }
    }
}
