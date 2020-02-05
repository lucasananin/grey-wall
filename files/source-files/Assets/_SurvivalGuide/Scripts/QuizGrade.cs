using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizGrade : MonoBehaviour
{
    // Valores;
    [SerializeField] float[] _notas;
    [SerializeField] int _comprimento = 0;
    [SerializeField] int _arredondamento = 10;

    private void Start()
    {
        print("Pressione a tecla SPACE para calcular.");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _notas = new float[_comprimento];

            for (int _i = 0; _i < _notas.Length; _i++)
            {
                _notas[_i] = Random.Range(0f, 10f);
                _notas[_i] = Mathf.Round(_notas[_i] * _arredondamento) / _arredondamento;
            }

            float _total = 0f;

            foreach (var _val in _notas)
            {
                _total += _val;
            }

            float _media = (_total) / _notas.Length;
            _media = Mathf.Round(_media * _arredondamento) / _arredondamento;

            if (_media >= 7f)
                print("Aprovado! Media: " + _media);
            else
                print("Reprovado! Media: " + _media);
        }
    }
}
