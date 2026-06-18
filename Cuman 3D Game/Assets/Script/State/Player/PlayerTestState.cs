using UnityEngine;

public class PlayerTestState : PlayerBaseState
{


    [SerializeField] private float timer = 5f;

    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Enter");
    }

    public override void Tick(float deltaTime)
    {
        Debug.Log("Tick");
        //Debug.Log(stateMachine.PlayerInput.movementValue);
    }

    public override void Exit()
    {
        Debug.Log("Exit");
    }

    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerTestState(stateMachine));
    }
}
