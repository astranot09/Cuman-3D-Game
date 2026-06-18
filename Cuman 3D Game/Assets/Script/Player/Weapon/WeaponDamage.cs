using UnityEngine;
public class WeaponDamage : MonoBehaviour
{

    [SerializeField] private float weaponDamage = 10f;
    [SerializeField] private StatManager statManager;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;

        // 2. Baru cek apakah objek lain tersebut bisa diberi damage
        if (other.TryGetComponent<IDamageable>(out IDamageable damageableTarget))
        {
            float totalDamage = statManager.DamageOutput(weaponDamage);

            statManager.AddTotalDamage(totalDamage);

            damageableTarget.TakeDamage(totalDamage);
            SoundManager.instance.PlaySFX(SoundManager.instance.swordDamage);
        }
    }

    public void SetWeaponDamage(float damage)
    {
        weaponDamage = damage;
    }
}
