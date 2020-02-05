using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeHUD : MonoBehaviour
{
    // Referencias;
    [SerializeField] ChargeWeapon _weaponScript = null;
    [SerializeField] Slider _chargeSlider = null;
    [SerializeField] Image _fillImage = null;
    [SerializeField] Color32 _loadingColor = Color.yellow;
    [SerializeField] Color32 _fullColor = Color.red;

    private void Start()
    {
        if (GameObject.Find("ChargeWeapon"))
            _weaponScript = GameObject.Find("ChargeWeapon").GetComponent<ChargeWeapon>();

        SetChargeSlider();
    }

    private void Update()
    {
        UpdateCharge();
    }

    private void UpdateCharge()
    {
        if (_weaponScript != null && _weaponScript._isCharging)
        {
            _chargeSlider.maxValue = _weaponScript._chargeToShot;
            _chargeSlider.value = _weaponScript._charger;

            if (_chargeSlider.value >= _chargeSlider.maxValue)
                _fillImage.color = _fullColor;
            else
                _fillImage.color = _loadingColor;
        }
    }

    private void SetChargeSlider()
    {
        if (_weaponScript != null)
        {
            _chargeSlider.maxValue = _weaponScript._chargeToShot;
            _chargeSlider.value = _weaponScript._charger;
        }
        else
        {
            _chargeSlider.maxValue = 1f;
            _chargeSlider.value = 0f;
        }
    }
}
