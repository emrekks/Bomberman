using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject playButtons;
    public GameObject optionsButtons;
    public Slider _masterSlider;
    public Slider _musicSlider;
    public Slider _sfxSlider;

    public AudioMixer _audioMixer;
    public Text masterVolumePercent;
    public Text musicVolumePercent;
    public Text sfxVolumePercent;

    public void OnPlayButton()
    {
        SceneManager.LoadScene("NameScreen");
    }
    
    public void OnOptionsButton()
    {
        playButtons.SetActive(false);
        optionsButtons.SetActive(true);
    }
    
    public void OnBackOptionsButton()
    {
        playButtons.SetActive(true);
        optionsButtons.SetActive(false);
    }
    
    public void OnQuitButton()
    {
        Application.Quit();
    }
    
    public void OnMasterValueChanged()
    {
        _audioMixer.SetFloat("MasterVolume", _masterSlider.value * 100);
        masterVolumePercent.text = "%" + Convert.ToInt32(_masterSlider.value * 100);
    }
    
    public void OnMusicValueChanged()
    {
        _audioMixer.SetFloat("MusicVolume", _musicSlider.value * 100);
        musicVolumePercent.text = "%" + Convert.ToInt32(_musicSlider.value * 100);
    }
    
    public void OnSFXValueChanged()
    {
        _audioMixer.SetFloat("SFXVolume", _sfxSlider.value * 100);
        sfxVolumePercent.text = "%" + Convert.ToInt32(_sfxSlider.value * 100);
    }
}
