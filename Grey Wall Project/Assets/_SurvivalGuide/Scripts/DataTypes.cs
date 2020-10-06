using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTypes : MonoBehaviour
{
    // Valores;
    public byte _byte = default;
    public short _short = default;
    public int _int = default;
    public long _long = default;
    public float _float = default;
    public double _double = default;
    public char _char = default;
    public string _string = default;
    public bool _bool = default;

    private void Start()
    {
        // Int para float;
        _int = 1;
        _float += _int;
        print("Int: " + _int + " To Float: " + _float);
        // Float para int;
        _float = 1;
        _int += (int)_float;
        print("Float: " + _float + " To Int: " + _int);
        // String para int;
        _string = "1";
        _int = int.Parse(_string);
        print("String: " + _string + " To Int: " + _int);
        // String para float;
        _string = "1,5";
        _float = float.Parse(_string);
        print("String: " + _string + " To Float: " + _float);
        // Int para string;
        _int = 1;
        _string = _int.ToString();
        print("Int: " + _int + " To String: " + _string);
        // Float para string;
        _float = 1.5f;
        _string = _float.ToString();
        print("Float: " + _float + " To String: " + _string);
        // String para char;
        _string = "A";
        _char = char.Parse(_string);
        print("String: " + _string + " To Char: " + _char);
        // Char para string;
        _char = 'B';
        _string = _char.ToString();
        print("Char: " + _char + " To String: " + _string);
    }
}
