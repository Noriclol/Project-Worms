using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    
    
    private InputMaster controls;
    
    
    
    
    
    
    
    
    
    
    //UI
    private InputAction pause;
    
    public static event Action OnPause = delegate {};
    
    
    //Player
    
    //PlayerMouse
    private InputAction mouseDelta;
    private InputAction mousebtn1;
    private InputAction mousebtn2;
    
    public static event Action<Vector2> OnMouseMove = delegate{};
    public static event Action OnMouseClick1 = delegate{};
    public static event Action OnMouseClick2 = delegate{};
    
    
    
    
    //PlayerKeyboard
    private InputAction move;
    private InputAction jump;
    private InputAction sWeapon;
    private InputAction sCam;
    
    
    public static event Action<Vector2> OnMove = delegate{};
    public static event Action OnJump = delegate {};
    public static event Action OnWeaponSwap = delegate {};
    public static event Action OnCameraSwap = delegate {};
    
    
    
    
    
    
    //TODO:   EVENT FUNCTIONS
    
    
    //UI
    private void Event_UI_Pause(InputAction.CallbackContext ctx) => OnPause();
    
    
    
    
    //Player Mouse
    private void Event_Mouse_Move(InputAction.CallbackContext ctx) => OnMouseMove(ctx.ReadValue<Vector2>());
    private void Event_Mouse_Btn1(InputAction.CallbackContext ctx) => OnMouseClick1();
    private void Event_Mouse_Btn2(InputAction.CallbackContext ctx) => OnMouseClick2();
    
    
    
    
    //Player Keys
    private void Event_Player_Move(InputAction.CallbackContext ctx) => OnMove(ctx.ReadValue<Vector2>()); 
    private void Event_Player_Jump(InputAction.CallbackContext ctx) => OnJump(); 
    private void Event_Player_SwapWeapon(InputAction.CallbackContext ctx) => OnWeaponSwap();
    private void Event_Player_SwapCamera(InputAction.CallbackContext ctx) => OnCameraSwap();
    

    
    
    
    private void Awake()
    {
        controls = new InputMaster();
    }

    private void OnEnable()
    {
        
        controls.Enable();
        
        
        //TODO: CONTOLSETUP
        
        //UI
        pause = controls.UI.Pause;
        
        pause.Enable();
        
        //PlayerMouse
        mouseDelta = controls.Game.Mouse;
        mousebtn1 = controls.Game.MouseBtn1;
        mousebtn2 = controls.Game.MouseBtn2;
        
        mouseDelta.Enable();
        mousebtn1.Enable();
        mousebtn2.Enable();
        
        //PlayerKeyboard
        move = controls.Game.Movement;
        jump = controls.Game.Jump;
        sWeapon = controls.Game.SwapWeapon;
        sCam = controls.Game.SwapCamera;
        
        move.Enable();
        jump.Enable();
        sWeapon.Enable();
        sCam.Enable();

        //TODO: EVENTBIND

        pause.performed += Event_UI_Pause;
        
        mouseDelta.performed += Event_Mouse_Move;
        mousebtn1.performed += Event_Mouse_Btn1;
        mousebtn2.performed += Event_Mouse_Btn2;
        
        move.performed += Event_Player_Move;
        jump.performed += Event_Player_Jump;
        sWeapon.performed += Event_Player_SwapWeapon;
        sCam.performed += Event_Player_SwapCamera;
        
    }

    private void OnDisable()
    {
        controls.Disable();
        
        pause.performed -= Event_UI_Pause;
        
        mouseDelta.performed -= Event_Mouse_Move;
        mousebtn1.performed -= Event_Mouse_Btn1;
        mousebtn2.performed -= Event_Mouse_Btn2;
        
        move.performed -= Event_Player_Move;
        jump.performed -= Event_Player_Jump;
        sWeapon.performed -= Event_Player_SwapWeapon;
        sCam.performed -= Event_Player_SwapCamera;
        
        
    }

}
