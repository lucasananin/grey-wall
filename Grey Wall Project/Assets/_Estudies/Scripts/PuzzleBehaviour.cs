using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBehaviour : MonoBehaviour
{
    // Referencias;
    [SerializeField] Camera _mainCam = null;

    // Valores;
    [Min(2)]
    [SerializeField] int _blocksX = 2;
    [Min(2)]
    [SerializeField] int _blocksY = 2;
    [Range(50f, 100f)]
    [SerializeField] float _screenDistance = 60f;

    // Mensagens;
    private void Start()
    {
        _mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent(typeof(Camera)) as Camera;
        _mainCam.orthographicSize = _blocksY * (_screenDistance / 100);
        CreateBlocks();
    }

    private void Update()
    {
        //_mainCam.orthographicSize = _blocksY * (_screenDistance / 100);
    }

    // Personalizados;
    private void CreateBlocks()
    {
        for (int _y = 0; _y < _blocksY; _y++)
        {
            for (int _x = 0; _x < _blocksX; _x++)
            {
                if (_x == _blocksX - 1 && _y == 0)
                {
                    continue;
                }

                GameObject _block = GameObject.CreatePrimitive(PrimitiveType.Quad);
                _block.transform.position = (-Vector2.one * (_blocksX - 1) * 0.5f) + new Vector2(_x, _y);
                _block.transform.parent = this.transform;
                //print(_block.transform.position);
            }
        }
    }
}
