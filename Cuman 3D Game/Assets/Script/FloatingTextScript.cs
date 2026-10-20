using UnityEngine;
using TMPro;
using DG.Tweening;

public class FloatingTextScript : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    [SerializeField] private float durationFloat = 1f;
    [SerializeField] private float floatDistance = 1.5f;

    private Camera mainCamera;

    private void Start()
    {
        // Cache kamera utama di awal agar tidak boros performa calling Camera.main di Update
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (mainCamera != null)
        {
            transform.rotation = mainCamera.transform.rotation;
        }
    }
    public void SetUpFloatingText(string value)
    {
        text.text = value;

        transform.DOMoveY(transform.position.y + floatDistance, durationFloat)
            .SetEase(Ease.OutQuad);

        text.DOFade(0f, durationFloat)
            .SetEase(Ease.InQuad);

        Destroy(gameObject, durationFloat);
    }
}
