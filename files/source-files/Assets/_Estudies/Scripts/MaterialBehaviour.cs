using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialBehaviour : MonoBehaviour
{
    // Referencias;
    [SerializeField] Color32 _defaultColor = Color.white;
    [SerializeField] Color32 _newColor = Color.red;
    private Renderer _rend = null;

    // Valores;
    [SerializeField] float _smoothTime = 5f;
    [SerializeField] bool _hasDamaged = false;

    private void Start()
    {
        _rend = GetComponentInChildren(typeof(Renderer)) as Renderer;
        _defaultColor = _rend.material.color;
    }

    private void Update()
    {
        UpdateColor();
    }

    private void UpdateColor()
    {
        _hasDamaged = Input.GetKeyDown(KeyCode.C);

        if (_hasDamaged)
        {
            //print("Tomou!");
            _rend.material.color = _newColor;
        }
        else
        {
            if (_rend.material.color == _defaultColor) return;
            //print("Recuperando!");
            _rend.material.color = Color.Lerp(_rend.material.color, _defaultColor, _smoothTime * Time.deltaTime);
        }
    }

    private void OnDestroy()
    {
        if (_rend != null)
            Destroy(_rend.material);
    }
}
