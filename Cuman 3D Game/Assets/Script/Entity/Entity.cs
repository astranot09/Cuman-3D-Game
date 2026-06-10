using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    // Menghubungkan variabel skrip ke Properti milik Interface
    public float MaxHealth => maxHealth;
    public float CurrentHealth => currentHealth;

    [SerializeField] private HealthBar healthBar;

    [Header("Floating Text")]
    [SerializeField] private GameObject damageTextPrefab;

    protected virtual void Start()
    {
        Debug.Log("Test A");
        currentHealth = maxHealth;
        Debug.Log("Test B");
        healthBar?.HealthBarInitiation();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        SpawnDamage(damage);

        Debug.Log($"{gameObject.name} terkena damage! Sisa nyawa: {currentHealth}");
        healthBar?.UpdateUI();

        TakingDamageLogic();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        // Logika mati (hancur, animasi drop item, dll)
        Destroy(gameObject);
    }


    private void SpawnDamage(float damage)
    {
        // 1. Ambil posisi spawn dasar (sedikit di atas posisi entity)
        Vector3 basePosition = transform.position + Vector3.up * 1.5f;

        // 2. Acak posisinya sedikit menggunakan fungsi RandomLocation
        Vector3 randomizedPosition = RandomLocation(basePosition);

        // 3. Spawn prefab floating text di posisi yang sudah diacak
        GameObject textGo = Instantiate(damageTextPrefab, randomizedPosition, Quaternion.identity);

        // 4. Ambil komponen FloatingTextScript dan set angka damage-nya
        FloatingTextScript floatingScript = textGo.GetComponent<FloatingTextScript>();
        if (floatingScript != null)
        {
            floatingScript.SetUpFloatingText(damage.ToString());
        }
    }

    private Vector3 RandomLocation(Vector3 baseLocation)
    {
        // Menentukan seberapa jauh teks boleh melenceng dari posisi dasar
        float xRandomRange = 1f;
        float yRandomRange = 0.3f;

        // Berikan nilai acak minus atau plus dari posisi dasar
        float randomX = Random.Range(-xRandomRange, xRandomRange);
        float randomY = Random.Range(-yRandomRange, yRandomRange);

        // Gabungkan posisi awal dengan nilai acak yang baru (Z tetap sama karena game 3D/2.5D biasanya konstan)
        Vector3 finalLocation = new Vector3(
            baseLocation.x + randomX,
            baseLocation.y + randomY,
            baseLocation.z
        );

        return finalLocation;
    }

    protected virtual void TakingDamageLogic()
    {

    }
}
