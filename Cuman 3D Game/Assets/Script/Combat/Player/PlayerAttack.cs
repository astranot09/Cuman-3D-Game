using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private ForceReceiver forceReceiver;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private float attackForce = 1f;

    private bool alreadyAppliedForced;

    private void OnEnable()
    {
        PlayerInput.AttackEvent += PlayAttack;
    }
    private void OnDisable()
    {
        PlayerInput.AttackEvent -= PlayAttack;
    }
    public void PlayAttack()
    {
        //Jadi, kalau udh dimainin anim Attacknya bakal di return
        if (!playerAnimator.CheckAnimationState("Attack3") ||( playerAnimator.CheckAnimationState("Idle") && playerAnimator.CheckAnimationState("Running"))) return;

        playerAnimator.AttackAnimation();
        playerMovement.SetMovement(false);
    }

    public void Attacking()
    {
        forceReceiver.AddForce(transform.forward * attackForce);
    }
    public void DoneAttacking()
    {
        playerMovement.SetMovement(true);
    }

}
