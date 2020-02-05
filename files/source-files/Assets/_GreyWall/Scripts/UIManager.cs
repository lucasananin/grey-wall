using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Componentes;
    [SerializeField]
    private Slider _healthSlider = null;
    [SerializeField]
    private Image _damagePanel = null;
    [SerializeField]
    private Text _timeText = null;
    [SerializeField]
    private Text _multiplierText = null;
    [SerializeField]
    private Text _ammoText = null;
    [SerializeField]
    private Text _powerUpText = null;
    [SerializeField]
    private Text _powerPointsText = null;
    [SerializeField]
    private GameObject _pausePanel = null;
    [SerializeField]
    private GameObject _deathScreen = null;
    public GameObject _newScorePanel = null;
    public NewScorePanel _newScoreScript = null;
    [SerializeField] private AudioSource _audioSource = null;
    private PlayerController _playerScript = null;
    private SpawnManager _enemySpawner = null;

    // Variaveis;
    public bool _isPaused = false;
    private bool _isPlayerDead = false;
    private int _playerScore = 0;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            _healthSlider.maxValue = _playerScript._maxhealth;
            _healthSlider.value = _playerScript._currentHealth;
        }

        _enemySpawner = GameObject.Find("EnemySpawner").GetComponent<SpawnManager>();
        InvokeRepeating("UpdateMultiplier", 0, 0.1f);
        InvokeRepeating("UpdatePowerPoints", 0, 0.1f);
    }
    
    void Update()
    {
        UpdateHealth();
        UpdateScore();
        //UpdateMultiplier();
        UpdateAmmo();
        //UpdatePowerUp();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused == false && _isPlayerDead == false)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    private void PauseGame()
    {
        _audioSource.Play();
        _isPaused = true;
        Time.timeScale = 0;
        _pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            _audioSource.Play();
        _isPaused = false;
        Time.timeScale = _enemySpawner._timeScale;
        _pausePanel.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void DeathScreen()
    {
        _isPlayerDead = true;
        StartCoroutine(DeathScreenRoutine());
    }

    IEnumerator DeathScreenRoutine()
    {
        yield return new WaitForSeconds(3);
        _deathScreen.SetActive(true);
    }

    public void NewScoreScreen()
    {
        _isPlayerDead = true;
        StartCoroutine(NewScoreRoutine());
    }

    IEnumerator NewScoreRoutine()
    {
        yield return new WaitForSeconds(3);
        _newScorePanel.SetActive(true);
        _newScoreScript.GetPlayerScore(_playerScore);
    }

    private void UpdateHealth()
    {
        if (_playerScript != null)
        {
            _healthSlider.value = _playerScript._currentHealth;

            if (_playerScript._damaged == true)
            {
                _damagePanel.color = new Color(1, 0, 0, 0.1f);
            }
            else
            {
                _damagePanel.color = Color.Lerp(_damagePanel.color, Color.clear, 5 * Time.deltaTime);
            }

            _playerScript._damaged = false;
        }
        else
        {
            _damagePanel.color = Color.Lerp(_damagePanel.color, Color.clear, 5 * Time.deltaTime);

            _healthSlider.value = 0;
        }
    }

    private void UpdateScore()
    {
        if (_playerScript == null) return;

        _timeText.text = "" + _playerScript._totalScore;
        _playerScore = _playerScript._totalScore;
    }

    private void UpdateMultiplier()
    {
        if (_playerScript == null) return;

        _multiplierText.text = _playerScript._scoreMultiplier + "x";
    }

    private void UpdateAmmo()
    {
        if (_playerScript == null) return;

        _ammoText.text = "" + (int)_playerScript._currentAmmo;
    }

    private void UpdatePowerUp()
    {
        if (_playerScript == null) return;

        _powerUpText.text = "" + _playerScript._powerUpID;
    }

    private void UpdatePowerPoints()
    {
        if (_playerScript == null) return;

        _powerPointsText.text = "" + _playerScript._powerPoints;
    }
}
