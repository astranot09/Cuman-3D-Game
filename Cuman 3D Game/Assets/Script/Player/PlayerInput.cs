using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{

    public event Action JumpEvent;
    public event Action DodgeEvent;


    [SerializeField] private PlayerMovement playerMovement;

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
}