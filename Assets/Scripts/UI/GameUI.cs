using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text Teamtxt;
    
    [SerializeField]
    private TMP_Text Playertxt;
    
    [SerializeField]
    private TMP_Text Turntxt;

    [SerializeField]
    private TMP_Text ShotsLefttxt;

    [SerializeField] 
    private GameObject WinBox;


    [SerializeField] 
    private TMP_Text WinText;
    
    
    
    [SerializeField] 
    private Slider HealthBar, StaminaBar;

    private PlayerController lastReferencedPlayer;
    
    
    private void OnEnable()
    {
        Main.GameManager.Event_UIUpdate += Rebind;


        

        Main.GameManager.Event_EndGame += DisplayWinBox;
    }

    private void OnDisable()
    {
        Main.GameManager.Event_UIUpdate -= Rebind;
        
        // int n = Main.GameManager.Selected;
        // Main.GameManager.players[n].EventWeaponUpdate -= UpdateShots;
        
        
        Main.GameManager.Event_EndGame -= DisplayWinBox;

    }

    private void Start()
    {
        Main.GameManager.Players[Main.GameManager.Selected].EventWeaponUpdate += UpdateShots;
    }

    public void Rebind(PlayerController playerController)
    {

        var player = playerController.player;
        
        Teamtxt.text = $"Team: {player.teamID}";
        Playertxt.text = $"Player: {player.playerID}";
        Turntxt.text = $"Turn: {Main.GameManager.turn}";

        
        HealthBar.value = player.health / 100f;
        StaminaBar.value = player.stamina / 100f;


        lastReferencedPlayer = playerController;
        
        UpdateShots(playerController);
    }

    public void UpdateShots(PlayerController playerController)
    {
        if (lastReferencedPlayer)
        {
            ShotsLefttxt.text = $"Shot left: {lastReferencedPlayer.currentShots.ToString()}/{lastReferencedPlayer.currentAllowedShots}";
        }
    }
    
    private void DisplayWinBox()
    {
        WinBox.SetActive(true);

        var teamIndex = Main.GameManager.winningTeam;
        var teamString = "";
        switch (teamIndex)
        {
            case 0:
                teamString = "Blue";
                break;
            
            case 1:
                teamString = "Red";
                break;
            
            case 2:
                teamString = "Green";
                break;
            
            case 3:
                teamString = "Yellow";
                break;
        }
        
        WinText.text = $"Team {teamString} wins the game!";
        WinText.color = Main.GameManager.teamMaterials[teamIndex].color;
    }
    
    public void Update()
    {
        StaminaBar.value = lastReferencedPlayer.player.stamina / 100f;
    }
}
