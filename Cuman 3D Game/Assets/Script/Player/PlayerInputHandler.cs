using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public static event Action AttackEvent;
    public static event Action JumpEvent;
    public static event Action DodgeEvent;

    [SerializeField] private PlayerInput unityPlayerInput;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerInteract playerInteract;
    [SerializeField] private PlayerSkill playerSkill;
    [SerializeField] private DialogueUI dialogueUI;

    public Vector2 movementValue { get; private set; }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        // Read as Vector2 (X = Right/Left, Y = Forward/Backward)
       Vector2 movementValue = ctx.ReadValue<Vector2>();

        // Pass it to movement
        playerMovement.SetMoveDirection(movementValue);
    }


    public void OnJump(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
            JumpEvent?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            DodgeEvent?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            AttackEvent?.Invoke();
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
            playerInteract.PlayerIsInteract();
    }

    public void OnDash(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            playerSkill.OnDash();
    }
    public void OnShield(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            playerSkill.OnShield();
    }

    public void OnUltimate(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            playerSkill.OnUltimate();
    }

    public void OnUI(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            unityPlayerInput.actions["Player/Attack"].Disable();
            Cursor.lockState = CursorLockMode.None;
        }
            
        else if (ctx.canceled)
        {
            unityPlayerInput.actions["Player/Attack"].Enable();
            Cursor.lockState = CursorLockMode.Locked;
        }
            
    }

    public void OnDialogueClick(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
            dialogueUI.OnDialogueClick();
    }
    public void ChangeModeToDialogue()
    {
        unityPlayerInput.SwitchCurrentActionMap("Dialogue");
    }
    public void ExitDialogueMode()
    {
        unityPlayerInput.SwitchCurrentActionMap("Player");
    }
}