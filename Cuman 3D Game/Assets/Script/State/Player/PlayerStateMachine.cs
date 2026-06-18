using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field : SerializeField] public PlayerInputHandler PlayerInput {  get; private set; }

    void Start()
    {
        SwitchState(new PlayerTestState(this));
    }
}
