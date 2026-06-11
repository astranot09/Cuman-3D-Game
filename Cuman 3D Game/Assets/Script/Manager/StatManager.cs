using UnityEngine;

public class StatManager : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private float baseAttack = 10f;
    public float BaseAttack => baseAttack;

    [Header("Crit Rate")]
    [SerializeField] private float critRate = 10f;
    public float CritRate => critRate;

    [Header("Crit Damage")]
    [SerializeField] private float critDamage = 50f;
    public float CritDamage => critDamage;


    [Header("Total Damage Counter")]
    [SerializeField] private float totalDamage;
    public float TotalDamage => totalDamage;

    [Header("UI")]
    [SerializeField] private UIManager uiManager;

    public float DamageOutput(float baseDamageWeapon)
    {
        float damageCalculation = (baseDamageWeapon + baseAttack);
        if (isCrit())
            damageCalculation = damageCalculation + damageCalculation * critDamage/100;

        return damageCalculation;
    }

    public bool isCrit()
    {
        float x = Random.Range(0, 100);
        if(x <= critRate)
        {
            return true;
        }
        else
            return false;
    }


    public void IncreaseDamage()
    {
        baseAttack += 10;
        uiManager.UpdateStatUI();
    }
    public void DecreaseDamage()
    {
        if(baseAttack <=0 ) return; 
        baseAttack -= 10;
        uiManager.UpdateStatUI();
    }



    public void IncreaseCritRate()
    {
        critRate += 5;
        uiManager.UpdateStatUI();
    }
    public void DecreaseCritRate()
    {
        if (critRate <= 0) return;
        critRate -= 5;
        uiManager.UpdateStatUI();
    }

    public void IncreaseCritDamage()
    {
        critDamage += 10;
        uiManager.UpdateStatUI();
    }
    public void DecreaseCritDamage()
    {
        if (critDamage <= 0) return;
        critDamage -= 10;
        uiManager.UpdateStatUI();
    }



    public void AddTotalDamage(float valueDamage)
    {
        totalDamage += valueDamage;
        uiManager.UpdateTotalDamage();
    }
    public void RestartTotalDamage()
    {
        totalDamage = 0;
        uiManager.UpdateTotalDamage();
    }
}
