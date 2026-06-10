using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field : SerializeField] public PlayerInput PlayerInput {  get; private set; }

    void Start()
    {
        // 1. Buat objek State baru, sekalian oper "this" (dirinya sendiri) lewat constructor
        PlayerTestState stateAwal = new PlayerTestState(this);

        // 2. Masukkan state tersebut ke dalam sistem StateMachine
        SwitchState(stateAwal);

        // Catatan: Biasanya para programmer menyingkatnya langsung menjadi:
        // SwitchState(new PlayerTestState(this));
    }
}
