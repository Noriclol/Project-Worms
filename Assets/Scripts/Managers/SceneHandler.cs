using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public bool isPaused = false;

    private void OnEnable() 
        => InputManager.OnPause += Pause;
    
    private void OnDisable() 
        => InputManager.OnPause -= Pause;
    

    public void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    public void LoadGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Additive);
        
        Main.InputManager.SetMouseState(InputManager.MouseState.Game);
    }
    
    
    public void Unload(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
    
    [ContextMenu("Pause")]
    public void Pause()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            Debug.Log("MainMenu is active");
            return; 
        }
            
        if (isPaused)
        {
            isPaused = false;
            SceneManager.UnloadSceneAsync("Pause");
            Main.InputManager.SetMouseState(InputManager.MouseState.Game);
        }
        else
        {
            isPaused = true;
            SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
            Main.InputManager.SetMouseState(InputManager.MouseState.UI);
        }
    }

    public void LoadDefaultScene()
    {
        Load("Menu");
        Main.InputManager.SetMouseState(InputManager.MouseState.UI);
    }
}


