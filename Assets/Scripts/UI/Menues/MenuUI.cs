using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public void PlayBtn()
    {
        Main.SceneHandler.Load("Map1");
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
