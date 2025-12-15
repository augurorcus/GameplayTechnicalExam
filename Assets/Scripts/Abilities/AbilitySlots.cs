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
    [SerializeField] private Image _cooldownImage;
 
    private Ability _ability;
    private Button _currentButton;

    public void Initialize(int slotNumber, Ability abilityToMap)
    {
        _inputText.text = slotNumber.ToString();
        _ability = abilityToMap;
        _assignedAbility = abilityToMap;
        _assignedAbility.OnStartAbility.AddListener(OnStartCooldown);
        _assignedAbility.AbilityCooldownHandler.OnTickTimer.AddListener(OnTick);
        _assignedAbility.AbilityCooldownHandler.OnStartTimer.AddListener(OnStartCooldown);
        _assignedAbility.AbilityCooldownHandler.OnEndTimer.AddListener(OnFinishedCooldown);
        _currentButton = GetComponent<Button>();
        _currentButton.onClick.AddListener(OnClick);
    }
    private async void OnClick()
    {
        await UseAbilitySlots();
    }

    private void OnStartCooldown(float value)
    {
        _cooldownImage.fillAmount = value;
    }

    private void OnTick(float value)
    {
        _cooldownImage.fillAmount = value;
    }

    private void OnFinishedCooldown(float value)
    {
        _cooldownImage.fillAmount = value;
    }


    public async UniTask UseAbilitySlots()
    {
        await _ability.TriggerAbilityBehavior();
    }

}
