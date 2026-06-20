using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class PlayerSkill : MonoBehaviour
{
    [Header("Shield SKill")]
    [SerializeField] private VisualEffect shieldEffect;
    [SerializeField] private bool shieldActivate;
    [SerializeField] private float shieldDuration;
    [SerializeField] private float shieldCooldown;
    private bool isShieldOnCooldown; // Tracks if the shield is cooling down
    public bool ShieldActivated => shieldActivate;

    [Header("Dash SKill")]
    [SerializeField] private float dashForced = 100f;
    [SerializeField] private int maxDash = 2;
    [SerializeField] private int currDash;
    [SerializeField] private float dashCooldown;
    [SerializeField] private Coroutine dashRecoveryCoroutine;


    [Header("Forced")]
    [SerializeField] private ForceReceiver forceReceiver;


    [Header("UI")]
    [SerializeField] private SkillUI skillUI;

    private void Start()
    {
        // Initialize dashes so the player starts with maximum charges
        currDash = maxDash;
        if (skillUI != null)
        {
            skillUI.UpdateShieldUI(1f); // 1 = penuh/siap
            skillUI.UpdateDashUI(1f, currDash);
        }
    }


    public void OnShield(InputAction.CallbackContext ctx)
    {
        Debug.Log("Shield");
        // Only trigger once on button press, if shield isn't active, and isn't on cooldown
        if (ctx.performed && !shieldActivate && !isShieldOnCooldown)
        {
            StartCoroutine(ShieldRoutine());
        }
    }

    private IEnumerator ShieldRoutine()
    {
        // 1. Aktifkan Shield
        shieldActivate = true;
        if (shieldEffect != null) shieldEffect.Play();

        // Selama durasi aktif, UI di-set ke 0 (menandakan shield sedang dipakai)
        if (skillUI != null) skillUI.UpdateShieldUI(0f);

        yield return new WaitForSeconds(shieldDuration);

        // 2. Matikan Shield
        shieldActivate = false;
        if (shieldEffect != null) shieldEffect.Stop();

        // 3. Masuk Cooldown (Transisi UI Mulus dari 0% ke 100%)
        isShieldOnCooldown = true;
        float cooldownTimer = 0f;

        while (cooldownTimer < shieldCooldown)
        {
            cooldownTimer += Time.deltaTime;

            if (skillUI != null)
            {
                // Menghitung persentase fillAmount (0.0 sampai 1.0)
                float fillRatio = cooldownTimer / shieldCooldown;
                skillUI.UpdateShieldUI(fillRatio);
            }
            yield return null; // Tunggu ke frame berikutnya (smooth)
        }

        // Pastikan penuh sempurna di akhir
        if (skillUI != null) skillUI.UpdateShieldUI(1f);
        isShieldOnCooldown = false;
    }


    public void OnDash(InputAction.CallbackContext ctx)
    {
        if (currDash > 0 && ctx.performed)
        {
            currDash--;

            // Update UI jumlah dash langsung saat digunakan
            if (skillUI != null) skillUI.UpdateDashUI(0f, currDash);

            forceReceiver.AddForce(transform.forward * dashForced);

            if (dashRecoveryCoroutine == null)
            {
                dashRecoveryCoroutine = StartCoroutine(DashRecoveryCountDown());
            }
        }
    }


    private IEnumerator DashRecoveryCountDown()
    {
        while (currDash < maxDash)
        {
            float recoveryTimer = 0f;

            // Transisi UI pengisian cooldown dash per charges secara mulus
            while (recoveryTimer < dashCooldown)
            {
                recoveryTimer += Time.deltaTime;

                if (skillUI != null)
                {
                    float fillRatio = recoveryTimer / dashCooldown;
                    skillUI.UpdateDashUI(fillRatio, currDash);
                }
                yield return null;
            }

            currDash++;
            Debug.Log($"Dash recovered! Current dashes: {currDash}");

            // Update UI lagi setelah angka dash resmi bertambah
            if (skillUI != null) skillUI.UpdateDashUI(1f, currDash);
        }

        dashRecoveryCoroutine = null;
    }
}
