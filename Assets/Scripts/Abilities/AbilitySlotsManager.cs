using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySlotsManager : Singleton<AbilitySlotsManager>
{
    [SerializeField] private AbilitySlots _abilitySlotsPrefab;

    [SerializeField] private SOListAbility _characterAbilityList;

    [SerializeField] private Transform _abilityButtonParent;

    private List<AbilitySlots> _currentActiveAbilitySlots;

    private void Start()
    {
        InitializeAbilities();
    }

    private void InitializeAbilities()
    {
        _currentActiveAbilitySlots = new List<AbilitySlots>();  

        for (int abilityIndex = 0; abilityIndex < _characterAbilityList.list.Count; abilityIndex++)
        {
            Ability currentAbility = _characterAbilityList.list[abilityIndex];
            currentAbility.InitializeAbility();
            AbilitySlots newAbilitySlot = Instantiate(_abilitySlotsPrefab, _abilityButtonParent);
            newAbilitySlot.Initialize(abilityIndex + 1, currentAbility);
            _currentActiveAbilitySlots.Add(newAbilitySlot);
        }
    }

    public async UniTask TriggerAbility(int slotNumber)
    {
        if (slotNumber <= 0) return;

        int indexedSlot = slotNumber - 1;
        await _currentActiveAbilitySlots[indexedSlot].UseAbilitySlots();
    }
}
