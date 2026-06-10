using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Entity entity;

    [SerializeField] private Slider healthSlider; // Slider Merah (Utama)
    [SerializeField] private Slider easeSlider;   // Slider Kuning (Bayangan)

    [Tooltip("Kecepatan slider kuning mengejar slider merah (Rekomendasi: 2 - 5)")]
    [SerializeField] private float lerpSpeed = 4f;

    public void HealthBarInitiation()
    {
        // Jalankan inisialisasi awal agar saat game mulai, UI langsung penuh sesuai darah Entity
        if (entity != null)
        {
            float maxHp = entity.MaxHealth;
            float currentHp = entity.CurrentHealth;

            healthSlider.maxValue = maxHp;
            healthSlider.value = currentHp;

            easeSlider.maxValue = maxHp;
            easeSlider.value = currentHp;
        }
    }



    // Fungsi ini dipanggil dari skrip Entity (misal di dalam fungsi TakeDamage)
    public void UpdateUI()
    {
        Debug.Log("Update UI");
        if (entity == null) return;

        // Selalu pastikan MaxValue sinkron (berguna jika nanti game kamu punya sistem Upgrade Max HP)
        healthSlider.maxValue = entity.MaxHealth;
        easeSlider.maxValue = entity.MaxHealth;

        // Slider merah langsung memotong darah secara instan
        healthSlider.value = entity.CurrentHealth;
    }

    private void Update()
    {
        // Cek jika slider kuning nilainya masih lebih besar dari slider merah
        if (easeSlider.value > healthSlider.value)
        {
            Debug.Log("XIXIXI");
            // Menggunakan Mathf.Lerp memberikan efek interpolasi yang mulus (smooth ease-out)
            easeSlider.value = Mathf.Lerp(easeSlider.value, healthSlider.value, lerpSpeed * Time.deltaTime);

            // Jika jarak keduanya sudah sangat kecil, langsung paksa samakan nilainya agar tidak terus mendatar
            if (easeSlider.value - healthSlider.value < 0.05f)
            {
                easeSlider.value = healthSlider.value;
            }
        }
    }
}