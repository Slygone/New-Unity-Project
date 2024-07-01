using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public LevelManager levelManager;

    //calling Advanture button
    public void OnAdventureButtonClicked()
    {
        if (levelManager != null)
        {
            levelManager.LoadAdventureGame();
        }
    }
    
    //calling Survival Button
    public void OnSurvivalButtonClicked()
    {
        if (levelManager != null)
        {
            levelManager.LoadSurvivalGame();
        }
    }

    //calling Quit Button
    public void OnQuitGameButtonClicked()
    {
        if (levelManager != null)
        {
            levelManager.QuitGame();
        }
    }
}
