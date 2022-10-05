using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    //refs
    public GameObject playerPrefab;
    public GameObject cameraPrefab;


    private List<Color> teamColors;
    //Instance Management
    public List<PlayerController> players;
    public List<Player> activePlayers;
    
    
    
    
    public GameObject GameCamera;
    
    public int playerCount = 1;
    public int teamCount = 2;
    
    public int Selected = 0;
    public float playerSpawnAreaSize = 10f;
    public int turn = 0;
    
    
    //events

    //public event NextTurn
    
    
    public event Action Event_NextTurn = delegate {};
    public event Action Event_NextPlayer = delegate {};
    public event Action Event_EndGame = delegate {};
    
    public event Action<PlayerController> Event_UIUpdate = delegate {};
    
    
    
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
                
                var newPlayerScript = newPlayer.GetComponent<Player>();
                newPlayerScript.playerID = players.Count;
                newPlayerScript.teamID = j;
                
                players.Add(newPlayerController);
                activePlayers.Add(newPlayerScript);
            }
        }
        PlayerBind(Selected);
    }
    
    
    
    
    //PlayerManipulation

    public void PlayerBind(int n)
    {

        //binds

        //Mouse
        InputManager.OnMouseClick1 +=
            players[n].Shoot; //InputManager.OnMouseClick1 += players.ElementAtOrDefault(n)!.Shoot;
        InputManager.OnMouseClick2 += players[n].Aim;
        //Keyboard
        InputManager.OnMove += players[n].UpdateMoveVec;
        InputManager.OnJump += players[n].Jump;
        InputManager.OnWeaponSwap += players[n].SwitchWeapon;


        //virtualCamera
        players[n].EnableCamera();
        players[n].Selected = true;

        Event_UIUpdate(players[Selected]);
    }

    public void PlayerUnbind(int n)
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
        players[n].DisableCamera();
        players[n].Selected = false;
    }

    
    [ContextMenu("Next Player")]
    public void PlayerNext()
    {
        if (Selected + 1 <= players.Count - 1)
        {
            PlayerUnbind(Selected);
            
            //Iterate to next active player
            Selected++;
            while (Selected != activePlayers[Selected].playerID)
            {
                Selected++;
            }
            

            PlayerBind(Selected);
            
            Event_NextPlayer();
        }
        else
        {
            PlayerUnbind(Selected);
            
            Selected = 0;
            turn++;
            
            PlayerBind(Selected);

            Event_NextPlayer();
            Event_NextTurn();

        }
    }
    
    


    public void PlayerDeath(int playerID)
    {
        Destroy(players[playerID].gameObject);
        players.RemoveAt(playerID);
        CheckTeamsAlive();
    }


    public void CheckTeamsAlive()
    {
        int teamsAliveCounter;
        List<int> teamsFound = new List<int>();

        foreach (var player in activePlayers)
        {
            var ID = player.teamID;

            if (teamsFound.Contains(ID))
                continue;

            else
                teamsFound.Add(ID);
        }

        teamsAliveCounter = teamsFound.Count;

        if (teamsAliveCounter <= 1)
            EndGameSession();
    }

    public void EndGameSession()
    {
        
        print($"Only 1 team left, ending session");
    }

}



