using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserInterface : MonoBehaviour
{
    private int enemyCount;
    public Text enemyCount_text;

    private bool inRoutine_heart = false;
    public int heartAnimIdx = 0;
    public Sprite[] HeartAnimationLoop;
    public Image heartImage;

    private GameLogic GL;
    public Text timer;

    public Image powerBuffImage;
    public Image speedBuffImage;

    public Button interactButton;

    public Image engGameTextBackground;
    public Text endGameText;
    
    private void Start()
    {
        EventHandler.curr.onEnemyDeath += ReduceEnemyCount;
        EventHandler.curr.onSpeedBuff += DisplaySpeedBuff;
        EventHandler.curr.onPowerBuff += DisplayPowerBuff;
        EventHandler.curr.onDoorTriggerEnter += EnableInteractButton;
        EventHandler.curr.onDoorTriggerExit += DisableInteractButton;
        EventHandler.curr.onPlayerInteract += PlayerInteracted;

        GL = GameObject.FindGameObjectsWithTag("GameLogic")[0].GetComponent<GameLogic>();
        DisplayTimer();

        enemyCount = GameObject.FindGameObjectsWithTag("BasicPiggy").Length;
        setEnemyText();
    }

    private void OnDestroy()
    {
        EventHandler.curr.onEnemyDeath -= ReduceEnemyCount;
        EventHandler.curr.onSpeedBuff -= DisplaySpeedBuff;
        EventHandler.curr.onPowerBuff -= DisplayPowerBuff;
        EventHandler.curr.onDoorTriggerEnter -= EnableInteractButton;
        EventHandler.curr.onDoorTriggerExit -= DisableInteractButton;
        EventHandler.curr.onPlayerInteract -= PlayerInteracted;
    }

    private void Update()
    {
        HeartLoop();
        DisplayTimer();
    }

    #region enemy counter
    void ReduceEnemyCount()
    {
        enemyCount -= 1;
        setEnemyText();
    }

    private void setEnemyText()
    {
        enemyCount_text.text = "Enemies: " + this.enemyCount.ToString();
    }
    #endregion

    #region HeartLoop

    private void HeartLoop()
    {
        if(!inRoutine_heart)
            StartCoroutine(HeartAnim());
    }
    
    IEnumerator HeartAnim()
    {
        inRoutine_heart = true;
        yield return new WaitForSeconds(0.1f);
        heartImage.sprite = HeartAnimationLoop[heartAnimIdx];
        
        if (heartAnimIdx < HeartAnimationLoop.Length-1)
            heartAnimIdx++;
        
        else
            heartAnimIdx = 0;
        
        

        inRoutine_heart = false;
    }

    #endregion

    #region Timer

    void DisplayTimer()
    {
        timer.text = "Timer: " + Util.FormatTime((float)System.Math.Round(GL.getTimer()));
    }

    #endregion

    #region buffs

    private void DisplayPowerBuff(float duration)
    {
        powerBuffImage.fillAmount = 1;
        StartCoroutine(PowerBuffDuration(duration));
    }


    IEnumerator PowerBuffDuration(float maxduration)
    {
        float currDur = 0;
        float segment = 0.1f;

        while(currDur <= maxduration)
        {
            yield return new WaitForSeconds(segment);
            currDur += segment;
            powerBuffImage.fillAmount = 1 - Util.NumToPerc(currDur, maxduration);
        }
    }

    private void DisplaySpeedBuff(float duration)
    {
        speedBuffImage.fillAmount = 1;
        StartCoroutine(SpeedBuffDuration(duration));
    }

    IEnumerator SpeedBuffDuration(float maxduration)
    {
        float currDur = 0;
        float segment = 0.1f;
        while(currDur <= maxduration)
        {
            yield return new WaitForSeconds(segment);
            currDur += segment;
            speedBuffImage.fillAmount = 1 - Util.NumToPerc(currDur, maxduration);
        }
    }

    #endregion

    #region Interact Buttons

    private void EnableInteractButton()
    {
        Debug.Log("interact button enabled?");
        interactButton.gameObject.SetActive(true);
    }

    private void DisableInteractButton()
    {
        Debug.Log("Disable Interact Button");

        interactButton.gameObject.SetActive(false);
    }

    public void PlayerInteracted()
    {
        Debug.Log("Player Pressed theInteractButton");

        DisableInteractButton();
        GL.StopTimer();
        DisplayEndGameCard();
    }

    public void GoToStartMenu()
    {
        StartCoroutine(LoadScnene("StartMenu"));
    }

    public void ResetLevel()
    {
        GL.RestartLevel();
    }

    IEnumerator LoadScnene(string ScneneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(ScneneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    #endregion

    #region End Game Display 
    
    public void DisplayEndGameCard()
    {
        engGameTextBackground.gameObject.SetActive(true);
        string displayText = $"Level Completed\nTime : {Util.FormatTime((float)System.Math.Round(GL.getTimer()))}";
        endGameText.text = displayText;
    }

    #endregion

}
