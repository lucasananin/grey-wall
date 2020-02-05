using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesBehaviour : MonoBehaviour
{
    // Referencias;
    [SerializeField] private AudioSource _audioSource = null;

    // Variaveis;
    [SerializeField]
    private int _particlesID = 0;

    private void Start()
    {
        if (_particlesID == 0)
            Destroy(this.gameObject, 1f);

        if (_particlesID == 1)
            Destroy(this.gameObject, 2f);

        _audioSource.playOnAwake = true;
    }
}
