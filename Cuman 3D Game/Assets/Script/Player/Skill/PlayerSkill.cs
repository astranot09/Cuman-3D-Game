using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class PlayerSkill : MonoBehaviour
{
    [Header("Shield SKill")]
    [SerializeField] private ParticleSystem shieldEffect;
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

    [Header("Ultimate SKill")]
    [SerializeField] private float durationUltimate = 5f;
    [SerializeField] private float ultimateCooldown = 15f;
    [SerializeField] private Coroutine ultimateRecoveryCoroutine;
    [SerializeField] private GameObject ultimatePrefab;
    private bool onUltimate;

    [Header("Forced")]
    [SerializeField] private ForceReceiver forceReceiver;


    [Header("UI")]
    [SerializeField] private SkillUI skillUI;

    private void Start()
    {
        currDash = maxDash;
        if (skillUI != null)
        {
            skillUI.UpdateShieldUI(1f); // 1 = penuh/siap
            skillUI.UpdateDashUI(1f, currDash);
        }
    }


    public void OnShield()
    {
        Debug.Log("Shield");
        if (!shieldActivate && !isShieldOnCooldown)
        {
            StartCoroutine(ShieldRoutine());
        }
    }

    private IEnumerator ShieldRoutine()
    {
        shieldActivate = true;

        if (shieldEffect != null)
            shieldEffect.Play(true);

        if (skillUI != null) skillUI.UpdateShieldUI(0f);

        yield return new WaitForSeconds(shieldDuration);

        shieldActivate = false;
        if (shieldEffect != null)
            shieldEffect.Stop(true);

        isShieldOnCooldown = true;
        float cooldownTimer = 0f;

        while (cooldownTimer < shieldCooldown)
        {
            cooldownTimer += Time.deltaTime;

            if (skillUI != null)
            {
                float fillRatio = cooldownTimer / shieldCooldown;
                skillUI.UpdateShieldUI(fillRatio);
            }
            yield return null;
        }

        if (skillUI != null) skillUI.UpdateShieldUI(1f);
        isShieldOnCooldown = false;
    }


    public void OnDash()
    {
        if (currDash > 0)
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

    public void OnUltimate()
    {
        if (!onUltimate && ultimateRecoveryCoroutine == null)
        {
            ultimateRecoveryCoroutine = StartCoroutine(UltimateRecovery());
        }
    }

    IEnumerator UltimateRecovery()
    {
        onUltimate = true;
        Instantiate(ultimatePrefab,transform.position, Quaternion.identity);

        yield return new WaitForSeconds(durationUltimate);

        onUltimate = false;

        yield return new WaitForSeconds(ultimateCooldown);
        ultimateRecoveryCoroutine = null;
    }
}
