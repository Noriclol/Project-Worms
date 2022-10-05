using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    
    
    //fieldChanges

    public void ChangePlayerCount(int value)
    {
        Main.GameManager.playerCount = value + 1;
    }
    
    public void ChangeTeamCount(int value)
    {
        Main.GameManager.teamCount = value  + 2;
    }
    
    
    //Buttons
    
    public void PlayBtn()
    {
        Main.SceneHandler.LoadGame("CharacterTest");
    }

    public void SettingsBtn()
    {
        Main.SceneHandler.Load("Settings");
    }

    public void ExitBtn()
    {
        Application.Quit();
    }
}
