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
    public List<Material> teamMaterials;

    public List<GameObject> teamSpawns;
    
    //Instance Management
    public List<PlayerController> Players;
    
    public LevelManager LevelManager;
    
    
    public GameObject GameCamera;
    
    public int playerCount = 1;
    public int teamCount = 2;
    public int winningTeam = -1;
    
    
    public int Selected = 0;
    public float playerSpawnAreaSize = 10f;
    public int turn = 0;
    
    
    //events

    //public event NextTurn
    
    
    public event Action Event_NextTurn = delegate {};
    public event Action Event_NextPlayer = delegate {};
    public event Action Event_EndGame = delegate {};
    
    public event Action<PlayerController> Event_UIUpdate = delegate {};
    
    
    

    
    
    
    
    //INIT and CLEAR
    
    public void Init()
    {
        //fetch spawnpoints for number of teams
        for (int i = 0; i < teamCount; i++)
        {
            
            //add each team and set color to 
            teamSpawns.Add(LevelManager.spawnPoints[i]);
            teamSpawns[i].gameObject.GetComponentInChildren<MeshRenderer>().material.color = teamMaterials[i].color;
        }
        
        //camera Instantiation
        GameCamera = Instantiate(cameraPrefab);
        
        GeneratePlayersSetColors();
        
        PlayerBind(Selected);
    }
    
    
    public void ClearGameManager()
    {
        Players.Clear();
        teamSpawns.Clear();
        winningTeam = -1;
        turn = 0;
        Selected = 0;
    }
    
    
    
    
    Vector3 GetPosinArea(Vector3 origo)
    {
        return new Vector3(
            origo.x + Random.Range(playerSpawnAreaSize, -playerSpawnAreaSize),
            origo.y,
            origo.z + Random.Range(playerSpawnAreaSize, -playerSpawnAreaSize)
        );
    }
    
    
    
    private void GeneratePlayersSetColors()
    {
        for (int i = 0; i < playerCount; i++)
        {
            for (int j = 0; j < teamCount; j++)
            {
                var newPlayer = Instantiate(playerPrefab, GetPosinArea(teamSpawns[j].transform.position), Quaternion.identity);

                var newPlayerController = newPlayer.GetComponent<PlayerController>();
                newPlayerController.GameCamera = GameCamera.transform;
                newPlayerController.playerObj.GetComponent<MeshRenderer>().material = teamMaterials[j];

                var newPlayerScript = newPlayer.GetComponent<Player>();
                newPlayerScript.playerID = Players.Count;
                newPlayerScript.teamID = j;

                Players.Add(newPlayerController);
            }
        }
    }
    //PlayerManipulation

    public void PlayerBind(int n)
    {

        //binds

        //Mouse
        InputManager.OnMouseClick1 += Players[n].Shoot; //InputManager.OnMouseClick1 += players.ElementAtOrDefault(n)!.Shoot;
        InputManager.OnMouseClick2 += Players[n].Aim;
        //Keyboard
        InputManager.OnMove += Players[n].UpdateMoveVec;
        InputManager.OnJump += Players[n].Jump;
        InputManager.OnWeaponSwap += Players[n].SwitchWeapon;


        //virtualCamera
        Players[n].EnableCamera();
        Players[n].Selected = true;

        Event_UIUpdate(Players[Selected]);
    }

    public void PlayerUnbind(int n)
    {
        
        //Mouse
        InputManager.OnMouseClick1 -= Players[n].Shoot;
        InputManager.OnMouseClick2 -= Players[n].Aim;
        //Keyboard
        InputManager.OnMove        -= Players[n].UpdateMoveVec;
        InputManager.OnJump        -= Players[n].Jump;
        InputManager.OnWeaponSwap  -= Players[n].SwitchWeapon;
        
        
        //virtualCamera
        Players[n].DisableCamera();
        Players[n].Selected = false;
        
        Players[n].player.stamina = 100f;
        Players[n].currentShots = 0;
        Players[n].weaponController.WeaponLock = false;

    }

    
    [ContextMenu("Next Player")]
    public void PlayerNext()
    {
        if (Selected < Players.Count - 1)
        {
            PlayerUnbind(Selected);
            
            //Iterate to next active player
            Selected++;

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
    
    


    public void PlayerDeath(PlayerController player)
    {
        if (player == Players[Selected])
        {
            PlayerNext();
        }
        
        Players.Remove(player);
        Destroy(player.gameObject);
        
        CheckTeamsAlive();
    }


    public void CheckTeamsAlive()
    {
        int teamsAliveCounter;
        List<int> teamsFound = new List<int>();

        foreach (var player in Players)
        {
            var ID = player.player.teamID;

            if (teamsFound.Contains(ID))
                continue;

            else
                teamsFound.Add(ID);
        }

        teamsAliveCounter = teamsFound.Count;

        print($"teams alive = {teamsAliveCounter}");


        if (teamsAliveCounter <= 1)
        {
            winningTeam = teamsFound[0];
            EndGameSession();
        }
    }

    public void EndGameSession()
    {
        Event_EndGame();
        //PlayerUnbind(Players.Count - 1);
        Main.InputManager.SetMouseState(InputManager.MouseState.UI);
        //ClearGameManager();
        print($"Only 1 team left, ending session");
    }


}



