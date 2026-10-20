using UnityEngine;

public class PlayerAttackState : PlayerGroundedState
{
    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);
    }
    public override void Exit()
    {
        base.Enter();
    }
}
