using UnityEngine;

public class Player : Entity
{
    [Header("Player Attack Settings")]
    [SerializeField] private float damageAmount = 10f;

    [Header("Player Weapon Settings")]
    [SerializeField] private WeaponDamage playerWeapon;


    [SerializeField] private PlayerSkill skill;

    protected override void Start()
    {
        base.Start();
        playerWeapon.SetWeaponDamage(damageAmount);
    }
    protected override void TakingDamageLogic()
    {
        base.TakingDamageLogic();
        SoundManager.instance.PlaySFX(SoundManager.instance.playerHurt);
    }
    protected override bool IsDamageBlocked()
    {
        // Jika skill null atau shield aktif, kembalikan nilai true (blokir damage)
        if (skill != null && skill.ShieldActivated)
        {
            // Kamu juga bisa menambahkan sfx tameng/shield di sini jika mau
            return true;
        }

        // Jika shield tidak aktif, jalankan sfx terluka dan kembalikan false (darah tetap berkurang)
        return false;
    }
}
