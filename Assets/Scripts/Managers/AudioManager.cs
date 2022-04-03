using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
    private UiSliderRef sliders;    // Se busca automaticamente

    [Header("Main Configuration"), Space]
    public AudioMixer audioMixer;
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private float lastVolMusic;
    [SerializeField] private float lastVolEffect;
    [SerializeField] private bool isMusic = true;
    [SerializeField] private bool isEffect = true;

    [Header("Music clips"), Space]
    [SerializeField] private AudioClip menuMusic = null;
    [SerializeField] private AudioClip[] gameplayMusics = null;

    public void OnStart()
    {
        FindUiSliderRef();
        SetSliderValue();

        sliders.buttonMusic.image.color = isMusic ? Color.green : Color.red;
        sliders.buttonEffect.image.color = isEffect ? Color.green : Color.red;
    }
    void SetSliderValue()
    {
        float volume = 0;

        audioMixer.GetFloat("VolMusic", out volume);
        sliders.sliderMusic.value = volume;

        audioMixer.GetFloat("VolEffect", out volume);
        sliders.sliderEffect.value = volume;
    }
    public void FindUiSliderRef()
    {
        sliders = FindObjectOfType<UiSliderRef>();
        Invoke(nameof(SetSliderValue), 0.1f);
    }
    public void SetMusicVolume(float volume)
    {
        lastVolMusic = volume;
        audioMixer.SetFloat("VolMusic", volume);
    }
    public void SetEffectVolume(float volume)
    {
        lastVolEffect = volume;
        audioMixer.SetFloat("VolEffect", volume);
    }
    public void AlternateAudioMusic()
    {
        isMusic = !isMusic;
        audioMixer.SetFloat("VolMusic", isMusic ? lastVolMusic : -80);
        sliders.buttonMusic.image.color = isMusic ? Color.green : Color.red;
    }
    public void AlternateAudioEffect()
    {
        isEffect = !isEffect;
        audioMixer.SetFloat("VolEffect", isEffect ? lastVolEffect : -80);
        sliders.buttonEffect.image.color = isEffect ? Color.green : Color.red;
    }
    public void PlayGameplayMusic(int level)
    {
        audioSource.clip = gameplayMusics[level];
        audioSource.Play();
    }
    public void PlayMenuMusic()
    {
        audioSource.clip = menuMusic;
        audioSource.Play();
    }
}