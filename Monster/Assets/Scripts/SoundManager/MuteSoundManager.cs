using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteSoundManager : MonoBehaviour
{
    public GameObject soundCheck;
    public GameObject effectCheck;
    public Slider soundSlider;
    public Slider effectSlider;

    private void Start()
    {
        soundCheck.GetComponent<Toggle>().isOn = PlayerPrefs.GetInt("Sound") == 1 ? true : false;
        effectCheck.GetComponent<Toggle>().isOn = PlayerPrefs.GetInt("Effect") == 1 ? true : false;
        soundSlider.value = PlayerPrefs.GetFloat("SoundSlider");
        soundSlider.value = PlayerPrefs.GetFloat("EffectSlider");
    }
    private void Update()
    {
        AudioChecker(soundCheck, "Sound");
        AudioChecker(effectCheck, "Effect");
        SoundsSliderChanger();
        EffectsSliderChanger();
       // PlayerPrefs.SetFloat("SoundSlider", soundSlider.value);
        //PlayerPrefs.SetFloat("EffectSlider", effectSlider.value);

    }
    public void AudioChecker(GameObject audioCheck, string namePref)
    {
        if (audioCheck.GetComponent<Toggle>().isOn) 
        {
            PlayerPrefs.SetInt(namePref, 1);
        }
        else
        {
            PlayerPrefs.SetInt(namePref, 0);
        }
    } 

    public void SoundsSliderChanger()
    {
        PlayerPrefs.SetFloat("SoundSlider", soundSlider.value);
        Debug.Log(PlayerPrefs.GetFloat("SoundSlider"));
    }
    public void EffectsSliderChanger()
    {
        PlayerPrefs.SetFloat("EffectSlider", effectSlider.value);
        Debug.Log(PlayerPrefs.GetFloat("EffectSlider"));
    }

}
