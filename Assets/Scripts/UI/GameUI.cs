using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    
    [Header("Sliders")]
    
    [SerializeField] 
    private Slider HealthBar, StaminaBar;
    
    
    [Header("Weapon")]
    
    [SerializeField]
    private TMP_Text ShotsLefttxt;
    
    [SerializeField] 
    private TMP_Text WeaponLockText;
    
    
    [Header("Info")]
    
    [SerializeField]
    private TMP_Text Teamtxt;
    
    [SerializeField]
    private TMP_Text Playertxt;
    
    [SerializeField]
    private TMP_Text Turntxt;

    [Header("WinPanel")]

    [SerializeField] 
    private GameObject WinBox;
    
    [SerializeField] 
    private TMP_Text WinText;

    
    
    private void OnEnable()
    {


        Main.GameManager.Event_UI_Shots += SetShotsLeftText;
        Main.GameManager.Event_UI_WeaponLock += SetWeaponLockText;
        
        Main.GameManager.Event_UI_Health += SetHealthSlider;
        Main.GameManager.Event_UI_Stamina += SetStaminaSlider;

        Main.GameManager.Event_UI_Player += SetPlayerText;
        Main.GameManager.Event_UI_Team += SetTeamText;
        Main.GameManager.Event_UI_Turn += SetTurnText;
        
        
        Main.GameManager.Event_EndGame += DisplayWinBox;
    }

    private void OnDisable()
    {
        
        Main.GameManager.Event_UI_Shots -= SetShotsLeftText;
        Main.GameManager.Event_UI_WeaponLock -= SetWeaponLockText;
        
        Main.GameManager.Event_UI_Health -= SetHealthSlider;
        Main.GameManager.Event_UI_Stamina -= SetStaminaSlider;

        Main.GameManager.Event_UI_Player -= SetPlayerText;
        Main.GameManager.Event_UI_Team -= SetTeamText;
        Main.GameManager.Event_UI_Turn -= SetTurnText;
        
        
        
        Main.GameManager.Event_EndGame -= DisplayWinBox;

    }

    private void Start()
    {
        //print("gameplayUILoaded");
    }

    public void ReturnBtn()
    {
        Main.SceneHandler.Load("Menu");
        Main.GameManager.ClearGameManager();
    }
    
    //WeaponPanel
    
    private void SetShotsLeftText(int shots, int maxShots)
    {
        //print("SHOTSLEFT_UI_UPDATED");
        ShotsLefttxt.text = $"Shot left: {maxShots - shots}";
        
    }


    private void SetWeaponLockText(bool value)
    {
        //print("WEAPONLOCK_UI_UPDATED");
        switch (!value)
        {
            case true:
                WeaponLockText.text = "[Unlocked]";
                WeaponLockText.color = Color.green;
                break;
            
            case false:
                WeaponLockText.text = "[Locked]";
                WeaponLockText.color = Color.red;
                break;
        }
    }

    
    
    //Slider Panel
    private void SetStaminaSlider(float value) =>
        StaminaBar.value = value / 100f;
    

    private void SetHealthSlider(float value) =>
        HealthBar.value = value / 100f;





    //Info Panel
    

    private void SetPlayerText(int value)
    {
        Playertxt.text = $"Player: {value}";
    }
    
    private void SetTeamText(int value)
    {
        Teamtxt.text = $"Team: {value}";
    }
    
    private void SetTurnText(int value)
    {
        Turntxt.text = $"Turn: {value}";
    }
    
    
    //Victory Panel
    
    private void DisplayWinBox() //works
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
}
