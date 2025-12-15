using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using Cysharp;

public class PlayerAbilitySlotsController : PlayerController<AbilitySlotsManager>
{
    private const string KeyboardInput = "Key:/Keyboard/";

    protected override void Awake()
    {
        base.Awake();
        playerInputActions.PlayerAbilityInput.AbilityTrigger.performed += OnAbilityTrigger;
    }

    private async void OnAbilityTrigger(InputAction.CallbackContext context)
    {
        string inputContext = context.control.ToString();
        string cleanedInput = inputContext.Replace(KeyboardInput, "");
        int inputNumber = int.Parse(cleanedInput);

        await controlledObject.TriggerAbility(inputNumber);
    }
}
