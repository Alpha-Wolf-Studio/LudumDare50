using UnityEngine;
using UnityEngine.UI;

public class UiSliderRef : MonoBehaviour
{
    public Slider sliderMusic;
    public Slider sliderEffect;

    public Button buttonMusic;
    public Button buttonEffect;

    private void Start()
    {
        AudioManager.Get().OnStart();
    }

    // Se llaman desde los botones:

    public void SetMusicVolume(float volume) => AudioManager.Get().SetMusicVolume(volume);
    public void SetEffectVolume(float volume) => AudioManager.Get().SetEffectVolume(volume);
    public void AlternateAudioMusic() => AudioManager.Get().AlternateAudioMusic();
    public void AlternateAudioEffect() => AudioManager.Get().AlternateAudioEffect();

    // ---------------------------
}