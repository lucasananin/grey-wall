using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebTimer : MonoBehaviour
{
    [SerializeField] FadeManager _fadeRef = null;
    [SerializeField] bool _hasCalled = false;

    private void Start()
    {
        PlayerPrefs.DeleteAll();
    }

    private void Update()
    {
        if ((int)Time.timeSinceLevelLoad == 150 && _hasCalled == false)
        {
            _hasCalled = true;
            _fadeRef.FadeStart();
        }
    }
}
