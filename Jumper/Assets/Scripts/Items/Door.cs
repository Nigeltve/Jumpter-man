using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    bool allEmeiesKilled = false;
    Animator anim;

    private void Start()
    {
        EventHandler.curr.onAllEnemiesDead += isAllDead;
        EventHandler.curr.onPlayerInteract += GoThroughDoorAnimation;
        anim = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        EventHandler.curr.onAllEnemiesDead -= isAllDead;
        EventHandler.curr.onPlayerInteract -= GoThroughDoorAnimation;
    }


    private void isAllDead()
    {
        Debug.Log("Door is interactable now");
        allEmeiesKilled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") &&  allEmeiesKilled)
        {
            EventHandler.curr.TriggerDoorAreaEntered();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && allEmeiesKilled)
        {
            EventHandler.curr.TriggerDoorAreaExit();
        }
    }   

    private void GoThroughDoorAnimation()
    {
        anim.SetTrigger("Interact");
        StartCoroutine(CloseDoorAnimation());
    }

    IEnumerator CloseDoorAnimation()
    {
        yield return new WaitForSeconds(2.5f);
        anim.SetTrigger("closeDoor");
    }
    
}
