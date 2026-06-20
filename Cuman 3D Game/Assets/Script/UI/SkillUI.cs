using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SkillUI : MonoBehaviour
{
    [Header("Shield")]
    [SerializeField] private Image shieldUI;

    [Header("Dash")]
    [SerializeField] private Image dashUI;
    [SerializeField] private TMP_Text dashCountUI;


    public void UpdateShieldUI(float x)
    {
        shieldUI.fillAmount = x;
    }

    public void UpdateDashUI(float x, int count)
    {
        dashUI.fillAmount = x;
        dashCountUI.text = count.ToString();
    }
}
