using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboards : MonoBehaviour
{
    // Referencias;
    public Text _firstName = null;
    public Text _firstScore = null;
    public Text _secondName = null;
    public Text _secondScore = null;
    public Text _thirdName = null;
    public Text _thirdScore = null;

    // Valores;
    public int _default1 = 300;
    public int _default2 = 200;
    public int _default3 = 100;

    private void Start()
    {
        CheckScore();
    }

    private void CheckScore()
    {
        string _playerName = PlayerPrefs.GetString("PlayerName");
        int _playerScore = PlayerPrefs.GetInt("PlayerScore", 0);

        int _value1 = PlayerPrefs.GetInt("FirstValue", _default1);
        int _value2 = PlayerPrefs.GetInt("SecondValue", _default2);
        int _value3 = PlayerPrefs.GetInt("ThirdValue", _default3);
        string _name1 = PlayerPrefs.GetString("FirstName", "Tom");
        string _name2 = PlayerPrefs.GetString("SecondName", "Ana");
        string _name3 = PlayerPrefs.GetString("ThirdName", "Gil");

        if (_playerScore > _value1)
        {
            PlayerPrefs.SetString("ThirdName", _name2);
            PlayerPrefs.SetInt("ThirdValue", _value2);

            PlayerPrefs.SetString("SecondName", _name1);
            PlayerPrefs.SetInt("SecondValue", _value1);

            PlayerPrefs.SetString("FirstName", _playerName);
            PlayerPrefs.SetInt("FirstValue", _playerScore);
        }
        else if (_playerScore > _value2 && _playerScore < _value1)
        {
            PlayerPrefs.SetString("ThirdName", _name2);
            PlayerPrefs.SetInt("ThirdValue", _value2);

            PlayerPrefs.SetString("SecondName", _playerName);
            PlayerPrefs.SetInt("SecondValue", _playerScore);
        }
        else if (_playerScore > _value3 && _playerScore < _value2)
        {
            PlayerPrefs.SetString("ThirdName", _playerName);
            PlayerPrefs.SetInt("ThirdValue", _playerScore);
        }

        _firstName.text = PlayerPrefs.GetString("FirstName", "Tom");
        _secondName.text = PlayerPrefs.GetString("SecondName", "Ana");
        _thirdName.text = PlayerPrefs.GetString("ThirdName", "Gil");
        _firstScore.text = PlayerPrefs.GetInt("FirstValue", _default1).ToString();
        _secondScore.text = PlayerPrefs.GetInt("SecondValue", _default2).ToString();
        _thirdScore.text = PlayerPrefs.GetInt("ThirdValue", _default3).ToString();
    }
    
    public void ResetScore()
    {
        PlayerPrefs.DeleteAll();

        _firstName.text = "Tom";
        _firstScore.text = _default1.ToString();
        _secondName.text = "Ana";
        _secondScore.text = _default2.ToString();
        _thirdName.text = "Gil";
        _thirdScore.text = _default3.ToString();
    }
}
