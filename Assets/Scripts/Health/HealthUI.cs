using System.Collections;
using System.Collections.Generic;
using UnityAtoms.Editor;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    private float _maxHealthValue;

    public void UpdateHealthUI(float value)
    {
        _healthSlider.value = value;
    }
}
