using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{

    public GameObject[] UI_elements;

    public void StartGame()
    {
        StartCoroutine(LoadScnene("MainLevel"));
    }

    IEnumerator LoadScnene(string ScneneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(ScneneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void viewMainMenu()
    {
        turnOnSpecificGameobject(0);
    }

    public void viewControles()
    {
        turnOnSpecificGameobject(1);
    }


    public void viewGameObjective()
    {
        turnOnSpecificGameobject(2);
    }

    

    private void turnOnSpecificGameobject(int idx)
    {
        switch (idx)
        {
            case 0: //view main menu
                Debug.Log("Back to Main Menu");
                ToggleCanvas(0);
                break;

            case 1:
                Debug.Log("Control Pannel");
                ToggleCanvas(1);
                break;

            case 2:
                Debug.Log("Ojbective pannel");
                ToggleCanvas(2);
                break;
            default:
                break;
        }
    }

    private void ToggleCanvas(int idx)
    {
        for (int i = 0; i < UI_elements.Length; i++)
        {
            if (i == idx)
                UI_elements[i].gameObject.SetActive(true);
            else
                UI_elements[i].gameObject.SetActive(false);
        }
    }
}
