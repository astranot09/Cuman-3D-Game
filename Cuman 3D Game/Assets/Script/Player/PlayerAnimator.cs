using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    [SerializeField] private Animator animator;

    public void SetVelocityXAnimation(float x)
    {
        animator.SetFloat("velocityX", x);
    }
    public void SetVelocityYAnimation(float y)
    {
        animator.SetFloat("velocityY", y);
    }

    public void AttackAnimation()
    {
        // 3. Jika sedang tidak menyerang, baru jalankan triggernya
        animator.SetTrigger("isAttack");
    }

    public bool CheckAnimationState(string animationName)
    {
        // 1. Ambil informasi state animasi pada layer 0 (base layer)
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // 2. Cek apakah state yang aktif saat ini bernama "Attack" atau tidak
        // (Ganti "Attack" dengan NAMA STATE BLOK ANIMASI kamu di dalam Animator, bukan nama triggernya)
        if (stateInfo.IsName(animationName))
        {
            // Jika sedang memutar animasi Attack, keluar dari fungsi (jangan eksekusi trigger)
            return false;
        }
        return true;
    }

}
