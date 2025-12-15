using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using TMPro;
using System;
using Cysharp.Threading.Tasks;

public class AbilitySlots : MonoBehaviour
{
    [SerializeField, ReadOnly] private Ability _assignedAbility;

    [SerializeField] private Image _iconText;
    [SerializeField] private TMP_Text _inputText;

    private Ability _ability;

    public void Initialize(int slotNumber, Ability abilityToMap)
    {
        _inputText.text = slotNumber.ToString();
        _ability = abilityToMap;
    }

    public async UniTask UseAbilitySlots()
    {
        await _ability.TriggerAbilityBehavior();
    }
}
