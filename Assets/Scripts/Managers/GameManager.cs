using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //refs
    public GameObject playerPrefab;
    public GameObject cameraPrefab;
    
    //Instance Management
    public List<PlayerController> players;
    public GameObject GameCamera;
    public int playerCount = 1;
    public int Selected = 0;
    public float playerSpawnAreaSize = 10f;



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
        
        //mouse lock
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        //camera Instantiation
        GameCamera = Instantiate(cameraPrefab);
        
        
        
        //Instantiate Players
        for (int i = 0; i < playerCount; i++)
        {
            var newPlayer = Instantiate(playerPrefab, GetPosinArea(), Quaternion.identity);
            players.Add(newPlayer.GetComponent<PlayerController>());
            players[i].GameCamera = GameCamera.transform;
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
        InputManager.OnCameraSwap  += players[n].SwitchCamera;
        InputManager.OnWeaponSwap  += players[n].SwitchWeapon;
        
        
        //virtualCamera
        players[n].VirtualCam.gameObject.SetActive(true);
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
        InputManager.OnCameraSwap  -= players[n].SwitchCamera;
        InputManager.OnWeaponSwap  -= players[n].SwitchWeapon;
        
        
        //virtualCamera
        players[n].VirtualCam.gameObject.SetActive(false);
    }



    public enum MouseMode
    {
        Game,
        UI
    }
}



