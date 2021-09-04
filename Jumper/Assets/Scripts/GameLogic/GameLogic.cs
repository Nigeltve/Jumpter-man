using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public int enemyCount;

    public float timer = 0;
    bool timerStart = false;

    private void Start()
    {
        EventHandler.curr.onPlayerDeath += StopTimer;
        EventHandler.curr.onEnemyDeath += ReduceEnemyCount;
        enemyCount = GameObject.FindGameObjectsWithTag("BasicPiggy").Length;

        StartCoroutine(StartTimer());
    }

    private void OnDestroy()
    {
        EventHandler.curr.onPlayerDeath -= StopTimer;
        EventHandler.curr.onEnemyDeath -= ReduceEnemyCount;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }


        if (timerStart)
        {
            timer += Time.deltaTime;
        }
    }

    void ReduceEnemyCount()
    {
        enemyCount -= 1;
        if(enemyCount == 0)
        {
            EventHandler.curr.TriggerAllEnemiesDead();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StartCoroutine(StartTimer());
    }

    #region Timer
    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(1f);

        timer = 0;
        timerStart = true;
    }


    public void StopTimer()
    {
        timerStart = false;
    }

    public float getTimer()
    {
        return timer;
    }

    #endregion

}
