using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    public GameObject playerPrefab;
    public List<PlayerController> players;
    public int playerCount = 1;
    public int Selected = 0;
    
    
    
    
    public void Init()
    {
        //Instantiate Players
        for (int i = 0; i < playerCount; i++)
        {
            var newPlayer = Instantiate(playerPrefab, Vector3.up, Quaternion.identity);
            players.Add(newPlayer.GetComponent<PlayerController>());
        }
        Bind(Selected);
    }

    public void Bind(int n)
    {
        InputManager.OnMove        += players[n].Move;
        InputManager.OnJump        += players[n].Jump;
        InputManager.OnMouseClick1 += players[n].Shoot;
    }
    public void Unbind(int n)
    {
        InputManager.OnMove        -= players[n].Move;
        InputManager.OnJump        -= players[n].Jump;
        InputManager.OnMouseClick1 -= players[n].Shoot;
    }

}

public enum CameraMode
{
    First,
    Third
}
