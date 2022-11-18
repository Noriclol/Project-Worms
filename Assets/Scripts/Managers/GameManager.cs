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
    public LinkedList<PlayerController> Players = new LinkedList<PlayerController>();
    private LinkedListNode<PlayerController> currentPlayer;
    
    
    
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
    
    
    //Events UI Slider panel
    
    public event Action<float> Event_UI_Health = delegate {};
    
    public event Action<float> Event_UI_Stamina = delegate {};
    
    
    //Events UI WeaponPanel
    
    public event Action<int, int> Event_UI_Shots = delegate {};
    public event Action<bool> Event_UI_WeaponLock = delegate {};
    
    
    
    //Events UI InfoPanel
    public event Action<int> Event_UI_Player = delegate {};
    
    public event Action<int> Event_UI_Team = delegate {};
    
    public event Action<int> Event_UI_Turn = delegate {};
    
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
        
        //fix UI
    }
    
    
    public void ClearGameManager()
    {
        Players.Clear();
        teamSpawns.Clear();
        winningTeam = -1;
        turn = 0;
        Selected = 0;
    }

    private void UpdateUI()
    {
        var sPlayer = currentPlayer.Value;
        
        
        Event_UI_Shots.Invoke(sPlayer.currentShots, sPlayer.currentAllowedShots);
        Event_UI_WeaponLock.Invoke(sPlayer.weaponController.WeaponLock);
        
        Event_UI_Health.Invoke(sPlayer.player.health);
        Event_UI_Stamina.Invoke(sPlayer.player.stamina);
        
        Event_UI_Player.Invoke(sPlayer.player.playerID);
        Event_UI_Team.Invoke(sPlayer.player.teamID);
        Event_UI_Turn.Invoke(turn);
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

                Players.AddLast(newPlayerController);
            }
        }
        currentPlayer = Players.First;
        PlayerBind();
    }
    //PlayerManipulation

    public void PlayerBind()
    {

        //binds

        //Mouse
        InputManager.OnMouseClick1 += currentPlayer.Value.Shoot; //InputManager.OnMouseClick1 += players.ElementAtOrDefault(n)!.Shoot;
        InputManager.OnMouseClick2 += currentPlayer.Value.Aim;
        //Keyboard
        InputManager.OnMove += currentPlayer.Value.UpdateMoveVec;
        InputManager.OnJump += currentPlayer.Value.Jump;
        InputManager.OnWeaponSwap += currentPlayer.Value.SwitchWeapon;


        //virtualCamera
        currentPlayer.Value.EnableCamera();
        currentPlayer.Value.Selected = true;


        currentPlayer.Value.EventUIUpdate += UpdateUI;

        currentPlayer.Value.InvokeUIUpdate();
        //Players[Selected].EventWeaponUpdate(this);
    }

    public void PlayerUnbind()
    {
        
        //Mouse
        InputManager.OnMouseClick1 -= currentPlayer.Value.Shoot;
        InputManager.OnMouseClick2 -= currentPlayer.Value.Aim;
        //Keyboard
        InputManager.OnMove        -= currentPlayer.Value.UpdateMoveVec;
        InputManager.OnJump        -= currentPlayer.Value.Jump;
        InputManager.OnWeaponSwap  -= currentPlayer.Value.SwitchWeapon;
        
        
        //virtualCamera
        currentPlayer.Value.DisableCamera();
        currentPlayer.Value.Selected = false;
        
        currentPlayer.Value.player.stamina = 100f;
        currentPlayer.Value.currentShots = 0;
        currentPlayer.Value.weaponController.WeaponLock = false;

        currentPlayer.Value.EventUIUpdate -= UpdateUI;
        
    }

    #region plznolook

    private void Update()
    {
        if (currentPlayer != null && currentPlayer.Value)
            currentPlayer.Value.InvokeUIUpdate();
    }

    #endregion



    [ContextMenu("Next Player")]
    public void PlayerNext()
    {
        PlayerUnbind();
        if (currentPlayer.Value == Players.Last.Value)
        {
            currentPlayer = Players.First;
            turn++;
            Event_NextTurn();
        }
        else
        {
            currentPlayer = currentPlayer.Next;
            
        }
        Event_NextPlayer();
        PlayerBind();
    }
    
    


    public void PlayerDeath(PlayerController player)
    {
        if (player == currentPlayer.Value)
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
        PlayerUnbind();
        playerCount = 1;
        teamCount = 2;
        Event_EndGame();
        Main.InputManager.SetMouseState(InputManager.MouseState.UI);
        //ClearGameManager();
        //print($"Only 1 team left, ending session");
    }


}



