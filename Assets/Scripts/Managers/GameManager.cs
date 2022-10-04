using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //refs
    public GameObject playerPrefab;
    public GameObject cameraPrefab;


    private List<Color> teamColors;
    //Instance Management
    public List<PlayerController> players;
    public GameObject GameCamera;
    public int playerCount = 1;
    public int teamCount = 2;
    public int Selected = 0;
    public float playerSpawnAreaSize = 10f;
    public int turn = 0;

    Vector3 GetPosinArea()
    {
        return new Vector3(
            Random.Range(playerSpawnAreaSize, -playerSpawnAreaSize),
            0,
            Random.Range(playerSpawnAreaSize, -playerSpawnAreaSize)
            );
    }
    
    public void Init()
    {
        
        
        //camera Instantiation
        GameCamera = Instantiate(cameraPrefab);
        
        //List Instantiations
        teamColors = new List<Color>();

        for (int i = 0; i < teamCount; i++)
        {
            teamColors.Add(Random.ColorHSV());
        }
        
        
        for (int i = 0; i < playerCount; i++) {
            for (int j = 0; j < teamCount; j++)
            {
                var newPlayer = Instantiate(playerPrefab, GetPosinArea(), Quaternion.identity);
                var newPlayerController = newPlayer.GetComponent<PlayerController>();
                
                newPlayerController.GameCamera = GameCamera.transform;
                newPlayerController.playerObj.GetComponent<MeshRenderer>().material.color = teamColors[j];
                
                
                players.Add(newPlayerController);
            }
        }
        BindPlayer(Selected);




    }
    
    public void BindPlayer(int n)
    {
        
        //binds
        
        //Mouse
        InputManager.OnMouseClick1 += players[n].Shoot;        //InputManager.OnMouseClick1 += players.ElementAtOrDefault(n)!.Shoot;
        InputManager.OnMouseClick2 += players[n].Aim;
        //Keyboard
        InputManager.OnMove        += players[n].UpdateMoveVec;
        InputManager.OnJump        += players[n].Jump;
        InputManager.OnWeaponSwap  += players[n].SwitchWeapon;
        
        
        //virtualCamera
        players[n].EnableCamera();
        players[n].Selected = true;
    }
    public void UnbindPlayer(int n)
    {
        
        //binds
        
        //Mouse
        InputManager.OnMouseClick1 -= players[n].Shoot;
        InputManager.OnMouseClick2 -= players[n].Aim;
        //Keyboard
        InputManager.OnMove        -= players[n].UpdateMoveVec;
        InputManager.OnJump        -= players[n].Jump;
        InputManager.OnWeaponSwap  -= players[n].SwitchWeapon;
        
        
        //virtualCamera
        players[n].EnableCamera();
        players[n].Selected = false;
    }

    
    [ContextMenu("Next Player")]
    public void NextPlayer()
    {
        if (Selected + 1 <= players.Count - 1)
        {
            UnbindPlayer(Selected);
            
            Selected++;
            
            BindPlayer(Selected);
        }
        else
        {
            UnbindPlayer(Selected);
            
            Selected = 0;
            turn++;
            
            BindPlayer(Selected);
        }
    }
}



