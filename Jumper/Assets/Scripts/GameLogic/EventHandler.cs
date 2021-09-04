using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public static EventHandler curr;

    private void Awake()
    {
        curr = this;
    }

    public event Action onEnemyDeath;
    public void TriggerOnEnemyDeath()
    {
        if (onEnemyDeath != null)
            onEnemyDeath();
    }

    public event Action onAllEnemiesDead;
    public void TriggerAllEnemiesDead()
    {
        if (onAllEnemiesDead != null)
        {
            onAllEnemiesDead();
        }
    }

    public event Action onPlayerDeath;
    public void TriggerOnPlayerDeath()
    {
        if (onPlayerDeath != null)
        {
            onPlayerDeath();
        }
    }

    public event Action<float> onSpeedBuff;
    public void TriggerAddSpeedBuff(float duration)
    {
        if (onSpeedBuff != null)
        {
            onSpeedBuff(duration);
        }
    }

    public event Action<float> onPowerBuff;
    public void TriggerAddPowerBuff(float duration)
    {
        if (onPowerBuff != null)
        {
            onPowerBuff(duration);
        }
    }

    public event Action onDoorTriggerEnter;
    public void TriggerDoorAreaEntered()
    {
        if(onDoorTriggerEnter != null)
        {
            onDoorTriggerEnter();
        }
    }

    public event Action onDoorTriggerExit;
    public void TriggerDoorAreaExit()
    {
        if(onDoorTriggerExit != null)
        {
            onDoorTriggerExit();
        }
    }

    public event Action onPlayerInteract;
    public void TriggerOnPlayerInteract()
    {
        if (onPlayerInteract != null)
        {
            onPlayerInteract();
        }
    }
}
