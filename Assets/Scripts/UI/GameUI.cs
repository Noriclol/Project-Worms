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
    private Slider HealthBar, StaminaBar;

    private PlayerController lastReferencedPlayer;
    
    
    private void OnEnable()
    {
        Main.GameManager.Event_UIUpdate += Rebind;
    }

    private void OnDisable()
    {
        Main.GameManager.Event_UIUpdate -= Rebind;
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
    }

    public void Update()
    {
        StaminaBar.value = lastReferencedPlayer.player.stamina / 100f;
    }
}
