using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public bool isPaused = false;
    
    [HideInInspector]
    public List<Scene> ValidPlayerScenes;
    [HideInInspector]
    public Scene CurrentSceneRef;

    public void Awake()
    {
        ValidPlayerScenes.Add(SceneManager.GetSceneByBuildIndex(3));
    }

    public void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        CurrentSceneRef = SceneManager.GetActiveScene();
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

        }
        else
        {
            isPaused = true;
            SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
        }
    }

    public void LoadDefaultScene()
    {
        Load("Menu");
    }
}


