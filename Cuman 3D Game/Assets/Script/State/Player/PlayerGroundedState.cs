using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    private GroundChecker groundChecker;

    public PlayerGroundedState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        PlayerInputHandler.JumpEvent += OnJump;
        PlayerInputHandler.AttackEvent += OnAttack;
    }

    public override void Tick(float deltaTime)
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        PlayerInputHandler.JumpEvent -= OnJump;
        PlayerInputHandler.AttackEvent -= OnAttack;
    }
    private void OnJump()
    {
        if(groundChecker.IsGrounded())
            stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    }

    private void OnAttack()
    {
        stateMachine.SwitchState(new PlayerAttackState(stateMachine));
    }
}
