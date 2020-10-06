using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipCalculator : MonoBehaviour
{
    // Valores;
    [SerializeField] float _money = 0f;
    [SerializeField] float _bill = 0f;
    [SerializeField] [Range(20f, 40f)] float _tipPercentage = 20f;

    private void Start()
    {
        _tipPercentage = Mathf.Clamp(_tipPercentage, 20f, 40f);
        float _gorjetinha = _bill * _tipPercentage / 100f;
        float _total = _bill + _gorjetinha;
        float _troco= _money - _total;
        print("Conta: R$ " + _bill);
        print("Gorjeta: R$ " + _gorjetinha + " (" + _tipPercentage + "%)");
        print("Total: R$ " + _total);
        print("Preco Pago: R$ " + _money);
        print("Troco: R$ " + _troco);
    }
}
