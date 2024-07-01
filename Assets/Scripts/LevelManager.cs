using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //load Advanture game mode
    public void LoadAdventureGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    //load Survival game mode
    public void LoadSurvivalGame()
    {
        SceneManager.LoadScene("Survival");
    }


    //quit game
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
