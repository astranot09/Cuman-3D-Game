using UnityEngine;

public class PlayerTestState : PlayerBaseState
{


    [SerializeField] private float timer = 5f;

    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override void Tick(float deltaTime)
    {
        //Debug.Log(stateMachine.PlayerInput.movementValue);
    }

    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerTestState(stateMachine));
    }
}
