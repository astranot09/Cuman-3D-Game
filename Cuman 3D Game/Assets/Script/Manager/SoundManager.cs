using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    [Header("AudioSource")]
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource sfx;


    [Header("BGM")]
    [SerializeField] private AudioClip bgmSound;

    [Header("SFX")]
    public AudioClip swordDamage;
    public AudioClip playerHurt;

    private void Start()
    {
        PlayBGM(bgmSound);
    }

    // Fungsi untuk memainkan BGM (Bisa ganti-ganti lagu lewat parameter)
    public void PlayBGM(AudioClip clip)
    {
        if (bgm == null || clip == null) return;

        bgm.clip = clip; // Masukkan clip ke AudioSource
        bgm.loop = true;  // Pastikan BGM looping (berulang)
        bgm.Play();
    }

    // Fungsi untuk memainkan SFX sekali putar (misal: suara dipukul)
    public void PlaySFX(AudioClip clip)
    {
        if (sfx == null || clip == null) return;

        // PlayOneShot membuat suara tidak terpotong kalau ada SFX lain yang menyala bersamaan
        sfx.PlayOneShot(clip);
    }

}
