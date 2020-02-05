using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewScorePanel : MonoBehaviour
{
    // Referencias;
    public InputField _idInput = null;
    public Button _doneButton = null;
    private FadeManager _fadeManager = null;
    private UIManager _uiManager = null;

    // Valores;
    private int _playerScore = 0;

    private void Start()
    {
        if (GameObject.Find("FadeEffect") != null)
            _fadeManager = GameObject.Find("FadeEffect").GetComponent<FadeManager>();

        if (GameObject.Find("Canvas") != null)
            _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    private void Update()
    {
        if (string.IsNullOrEmpty(_idInput.text) || _idInput.text.Length != _idInput.characterLimit)
        {
            _doneButton.interactable = false;
        }
        else
        {
            _doneButton.interactable = true;
        }
    }

    public void GetPlayerScore(int _scoreValue)
    {
        _playerScore = _scoreValue;
    }

    public void DoneButton()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetString("PlayerName", _idInput.text);
        PlayerPrefs.SetInt("PlayerScore", _playerScore);
        _fadeManager.FadeStart();
    }
}
