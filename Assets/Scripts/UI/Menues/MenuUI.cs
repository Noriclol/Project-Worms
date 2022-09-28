using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
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
