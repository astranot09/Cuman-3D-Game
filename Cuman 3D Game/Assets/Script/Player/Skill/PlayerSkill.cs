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

    private void Start()
    {
        // Initialize dashes so the player starts with maximum charges
        currDash = maxDash;
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
        // 1. Activate Shield
        shieldActivate = true;
        if (shieldEffect != null) shieldEffect.Play();
        Debug.Log("Shield Activated!");

        // 2. Wait for duration to end
        yield return new WaitForSeconds(shieldDuration);

        // 3. Deactivate Shield
        shieldActivate = false;
        if (shieldEffect != null) shieldEffect.Stop();
        Debug.Log("Shield Expired. Cooldown started...");

        // 4. Handle Cooldown
        isShieldOnCooldown = true;
        yield return new WaitForSeconds(shieldCooldown);
        isShieldOnCooldown = false;
        Debug.Log("Shield Ready again!");
    }
    public void OnDash(InputAction.CallbackContext ctx)
    {
        Debug.Log("Dash");
        if (currDash > 0 && ctx.performed)
        {
            currDash--;
            forceReceiver.AddForce(transform.forward * dashForced);
            if(dashRecoveryCoroutine == null)
            {
                dashRecoveryCoroutine = StartCoroutine(DashRecoveryCountDown());
            }
        }
    }


    private IEnumerator DashRecoveryCountDown()
    {
        // Keep recovering charges one by one until maxDash is reached
        while (currDash < maxDash)
        {
            yield return new WaitForSeconds(dashCooldown);
            currDash++;
            Debug.Log($"Dash recovered! Current dashes: {currDash}");
        }

        // Set back to null when done so it can be restarted later
        dashRecoveryCoroutine = null;
    }
}
