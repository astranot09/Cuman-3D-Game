using UnityEngine;
using TMPro;

public class FloatingTextScript : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    [SerializeField] private float moveSpeed = 2f; // Tambahan biar teksnya melayang ke atas

    private Camera mainCamera;

    private void Start()
    {
        // Cache kamera utama di awal agar tidak boros performa calling Camera.main di Update
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // 1. Membuat teks selalu menghadap ke kamera (Billboard Effect)
        if (mainCamera != null)
        {
            // Opsi A: Membuat teks menghadap ke posisi kamera
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                             mainCamera.transform.rotation * Vector3.up);

            // Opsi B (Alternatif kalau teksnya kebalik/cermin):
            // transform.rotation = mainCamera.transform.rotation;
        }

        // 2. Membuat teks melayang ke atas seiring berjalannya waktu
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }
    public void SetUpFloatingText(string value)
    {
        text.text = value;
        Destroy(gameObject, 2f);
    }
}
