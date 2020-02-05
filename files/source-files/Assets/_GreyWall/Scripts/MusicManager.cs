using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Referencias;
    [SerializeField] private AudioClip[] _musicClips = null;
    private AudioSource _audioSource = null;
    private GameObject _player = null;

    // Valores;
    private int _index = 0;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        _player = GameObject.FindGameObjectWithTag("Player");

        Invoke("FirstMusic", 1);
    }

    private void Update()
    {
        if (_player == null)
            _audioSource.volume -= Time.deltaTime * 0.1f;

        if (_audioSource.clip == null)
            return;

        if (!_audioSource.isPlaying)
            ChangeMusic();
    }

    private void FirstMusic()
    {
        _index = 2;
        _audioSource.clip = _musicClips[_index];
        _audioSource.Play();
        //_musicClips[2] = _musicClips[0];
        //_musicClips[0] = _audioSource.clip;
    }

    private void ChangeMusic()
    {
        int _lastIndex = _index;
        _index = Random.Range(0, _musicClips.Length);

        if (_index == _lastIndex)
        {
            _index++;

            if (_index > (_musicClips.Length - 1))
                _index = 0;
        }

        _audioSource.clip = _musicClips[_index];
        _audioSource.Play();

        //int _rndMusic = Random.Range(1, _musicClips.Length);
        //_audioSource.clip = _musicClips[_rndMusic];
        //_musicClips[_rndMusic] = _musicClips[0];
        //_musicClips[0] = _audioSource.clip;
        //_audioSource.Play();
    }
}
