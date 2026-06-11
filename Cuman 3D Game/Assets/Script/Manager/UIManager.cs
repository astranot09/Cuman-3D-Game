using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] private StatManager playerStatManager;

    [Header("UI Stat Player")]
    [SerializeField] private TMP_Text baseAttackValueText;
    [SerializeField] private TMP_Text baseCritRateValueText;
    [SerializeField] private TMP_Text baseCritDamageValueText;

    [Header("UI TotalDamage Player")]
    [SerializeField] private TMP_Text totalDamageText;

    [Header("UI Pause")]
    [SerializeField] private GameObject pausePanel;
    private void Start()
    {
        UpdateStatUI();
        UpdateTotalDamage();
    }

    public void UpdateStatUI()
    {
        baseAttackValueText.text = playerStatManager.BaseAttack.ToString();
        baseCritRateValueText.text = playerStatManager.CritRate.ToString();
        baseCritDamageValueText.text = playerStatManager.CritDamage.ToString();
    }

    public void UpdateTotalDamage()
    {
        totalDamageText.text = playerStatManager.TotalDamage.ToString();
    }

    public void Paused()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
    }

}
