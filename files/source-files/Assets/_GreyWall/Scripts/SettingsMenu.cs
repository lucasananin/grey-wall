using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    // Referencias;
    [SerializeField] AudioMixer _sspAudioMixer = null;
    [SerializeField] Slider[] _volumeSliders = null;
    [SerializeField] Text[] _percentageTexts = null;
    [SerializeField] Dropdown _resolutionsDropdown = null;
    [SerializeField] Dropdown _qualityDropdown = null;
    [SerializeField] Toggle _windowedToggle = null;

    Resolution[] _resolutions = null;

    private void Start()
    {
        CheckSettings();
        CheckResolutions();
    }

    private void CheckSettings()
    {
        _qualityDropdown.value = PlayerPrefs.GetInt("QualityLevel", 5);
        _volumeSliders[0].value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        _volumeSliders[1].value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        _volumeSliders[2].value = PlayerPrefs.GetFloat("EffectsVolume", 1f);

        if (PlayerPrefs.GetInt("IsFullScreen", 1) == 0)
        {
            _windowedToggle.isOn = false;
            Screen.fullScreen = false;
        }
        else
        {
            _windowedToggle.isOn = true;
            Screen.fullScreen = true;
        }
    }

    private void CheckResolutions()
    {
        _resolutions = Screen.resolutions;
        _resolutionsDropdown.ClearOptions();
        List<string> _resOptions = new List<string>();
        int _currentResIndex = 0;
        for (int i = 0; i < _resolutions.Length; i++)
        {
            string _option = _resolutions[i].width + " x " + _resolutions[i].height + " - " + _resolutions[i].refreshRate + "Hz";
            _resOptions.Add(_option);

            if (_resolutions[i].Equals(Screen.currentResolution) && _resolutions[i].refreshRate.Equals(Screen.currentResolution.refreshRate))
            {
                _currentResIndex = i;
            }
        }
        _resolutionsDropdown.AddOptions(_resOptions);
        _resolutionsDropdown.value = PlayerPrefs.GetInt("CurrentResolution", _currentResIndex);
        _resolutionsDropdown.RefreshShownValue();
    }

    public void SetResolution(int _resIndex)
    {
        Resolution _res = _resolutions[_resIndex];
        Screen.SetResolution(_res.width, _res.height, Screen.fullScreen, _res.refreshRate);
        PlayerPrefs.SetInt("CurrentResolution", _resIndex);
    }

    public void SetQuality(int _qualityIndex)
    {
        QualitySettings.SetQualityLevel(_qualityIndex);
        PlayerPrefs.SetInt("QualityLevel", _qualityIndex);
    }

    public void SetFullScreen(bool _isFullScreen)
    {
        Screen.fullScreen = _isFullScreen;

        if (_isFullScreen == false)
            PlayerPrefs.SetInt("IsFullScreen", 0);
        else
            PlayerPrefs.SetInt("IsFullScreen", 1);
    }

    public void SetMasterVolume(float _volume)
    {
        _sspAudioMixer.SetFloat("MasterVolume", Mathf.Log10(_volume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", _volume);
    }

    public void SetMusicVolume(float _volume)
    {
        _sspAudioMixer.SetFloat("MusicVolume", Mathf.Log10(_volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", _volume);
    }

    public void SetEffectsVolume(float _volume)
    {
        _sspAudioMixer.SetFloat("EffectsVolume", Mathf.Log10(_volume) * 20);
        PlayerPrefs.SetFloat("EffectsVolume", _volume);
    }

    public void SetMasterText(float _value)
    {
        _percentageTexts[0].text = Mathf.Round(_value * 100) + "%";
    }

    public void SetMusicText(float _value)
    {
        _percentageTexts[1].text = Mathf.Round(_value * 100) + "%";
    }

    public void SetEffectsText(float _value)
    {
        _percentageTexts[2].text = Mathf.Round(_value * 100) + "%";
    }
}
