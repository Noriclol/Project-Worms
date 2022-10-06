using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> spawnPoints;
    void Start()
    {
        Main.GameManager.LevelManager = this;
        Main.GameManager.Init();
    }

}
