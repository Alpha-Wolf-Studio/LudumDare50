using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
    [SerializeField] private ShipPlayer player;
    [SerializeField] private AudioSource musicSource;

    public AudioMixer audioMixer;

    private float maxVol = 20;
    private float minVol = -30;
    private float distVol;

    private UiSliderRef sliders;
    private List<float> sliderValue = new List<float>();

    public enum Sounds     // Esto se ordena dependiendo del array objectsSounds !!
    {
        OnDie,
        OnKillEnemies
    }
    public Sounds soundsOrder;
    [SerializeField] private AudioSource[] objectsSounds;

    // Start is called before the first frame update
    void Start()
    {
        sliderValue.Add(7);
        sliderValue.Add(6);
    }

    void SetSliderValue()
    {
        for (int i = 0; i < sliders.sliders.Length; i++)
        {
            sliders.sliders[i].value = sliderValue[i];
        }
    }

    public void FindUiSliderRef()
    {
        sliders = FindObjectOfType<UiSliderRef>();
        Invoke(nameof(SetSliderValue), 0.1f);
    }

    
    public void SetEffectVolume(float volume)
    {
        //distVol = Mathf.Abs(maxVol - minVol) / 10;
        //if (volume < 1)
        //{
        //    audioMixer[0].SetFloat("MasterVolume", -80);
        //}
        //else
        //{
        //    audioMixer[0].SetFloat("MasterVolume", minVol + volume * distVol);
        //}
    }
    public void SetMusicVolume(float volume)
    {
      
    }

}
