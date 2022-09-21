using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public void ResumeBtn()
    {
        Main.SceneHandler.Pause();
    }
    
    public void ReturnBtn()
    {
        Main.SceneHandler.Load("Menu");
    }
    
}
