using UnityEngine;
using UnityEngine.UI;

public class UiSliderRef : MonoBehaviour
{
    public Slider sliderMusic;
    public Slider sliderEffect;

    public Button buttonMusic;
    public Button buttonEffect;

    private void Awake()
    {
        sliderMusic.onValueChanged.AddListener(SetMusicVolume);
        sliderEffect.onValueChanged.AddListener(SetEffectVolume);

        buttonMusic.onClick.AddListener(AlternateAudioMusic);
        buttonEffect.onClick.AddListener(AlternateAudioEffect);
    }

    private void Start()
    {
        AudioManager.Get().OnStart();
    }

    private void OnDisable()
    {
        sliderMusic.onValueChanged.RemoveListener(SetMusicVolume);
        sliderEffect.onValueChanged.RemoveListener(SetEffectVolume);

        buttonMusic.onClick.RemoveListener(AlternateAudioMusic);
        buttonEffect.onClick.RemoveListener(AlternateAudioEffect);
    }

    // Se llaman desde los botones:

    public void SetMusicVolume(float volume) => AudioManager.Get().SetMusicVolume(volume);
    public void SetEffectVolume(float volume) => AudioManager.Get().SetEffectVolume(volume);
    public void AlternateAudioMusic() => AudioManager.Get().AlternateAudioMusic();
    public void AlternateAudioEffect() => AudioManager.Get().AlternateAudioEffect();

    // ---------------------------
}