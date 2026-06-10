using UnityEngine;

public class Player : Entity
{
    [Header("Player Attack Settings")]
    [SerializeField] private float damageAmount = 10f;

    [Header("Player Weapon Settings")]
    [SerializeField] private WeaponDamage playerWeapon;
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
}
