using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Music")]
    [SerializeField] private AudioClip bgmSound;
    [SerializeField] private AudioClip loseMusic;
    [SerializeField] private AudioClip bgm2Sound;

    [Header("SFX")]
    [SerializeField] private AudioClip buttonSFX;
    [SerializeField] private AudioClip amuletSFX;
    [SerializeField] private AudioClip magnifyingGlassSFX;
    [SerializeField] private AudioClip restoreSFX;
    [SerializeField] private AudioClip castSpellSFX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //SFX
    public void RestoreSFX()
    {
        sfxSource.clip = restoreSFX;
        sfxSource.Play();
    }

    public void PlayMagnifyingGlassSFX()
    {
        sfxSource.clip = magnifyingGlassSFX;
        sfxSource.Play();
    }

    public void PlayAmuletSFX()
    {
        sfxSource.clip = amuletSFX;
        sfxSource.Play();
    }

    public void PlayButtonSFX()
    {
        sfxSource.clip = buttonSFX;
        sfxSource.Play();
    }

    //MUSIC
    public void PlayBGMusic()
    {
        musicSource.clip = bgmSound;
        musicSource.Play();
    }

    public void PlayLoseMusic()
    {
        musicSource.clip = loseMusic;
        musicSource.Play();
    }

}
