using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Main : MonoBehaviour
{
    
    //References
    public static GameManager GameManager;
    public static SceneHandler SceneHandler;
    public static InputManager InputManager;
    public static PlayerManager PlayerManager;
    public LevelManager LevelManager;
    
    
    //CONFIC //will move to separate script when it becomes unwieldy
    private static bool RunStartScene = false;
    public static bool DebugMsg = true;
    
    
    //Singleton Stuff
    public static Main Instance { get { return instance; } }
    
    private static Main instance;
    
    private void Awake()
    {
        GameManager = GetComponent<GameManager>();
        SceneHandler = GetComponent<SceneHandler>();
        InputManager = GetComponent<InputManager>();
        PlayerManager = GetComponent<PlayerManager>();
        
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }
    
    
    //main Init for Project
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void MainInit()
    {
        LoadMain();
        if (RunStartScene)
        {
            SceneHandler.LoadDefaultScene();
 
        }
    }
    
    
    
    //Loading Main into Game
    public static void LoadMain()
    {
        GameObject main = GameObject.Instantiate(Resources.Load("Main")) as GameObject;
        GameObject.DontDestroyOnLoad(main);
        Debug.Log("Main Loaded");
    }

    
    
    
}
