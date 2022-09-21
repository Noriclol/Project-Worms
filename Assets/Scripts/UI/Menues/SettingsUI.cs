using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsUI : MonoBehaviour
{
    public void ReturnBtn()
    {
        Main.SceneHandler.Load("Menu");
    }
}
