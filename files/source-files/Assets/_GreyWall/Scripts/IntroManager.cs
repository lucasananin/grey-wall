using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    // Referencias;
    [SerializeField] AudioSource _audioSource = null;
    private FadeManager _fadeManager = null;
    private bool _hasPressed = false;

    private void Start()
    {
        _fadeManager = GetComponentInChildren<FadeManager>();
    }

    private void Update()
    {
        if (Input.anyKeyDown && _hasPressed == false)
        {
            _hasPressed = true;
            _audioSource.Play();
            _fadeManager.FadeStart();
        }
    }
}
