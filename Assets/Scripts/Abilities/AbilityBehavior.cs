using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AbilityBehavior : ScriptableObject
{
    [SerializeField] private List<AbilityPhase> _abillityPhases;

    public async UniTask ExecuteAbilityBehavior()
    {
        for (int abilityIndex = 0; abilityIndex < _abillityPhases.Count; abilityIndex++)
        {
            AbilityPhase currentAbilityPhase = _abillityPhases[abilityIndex];
            currentAbilityPhase.StartAbilityPhase();
            await currentAbilityPhase.ProcessConsequences();
            currentAbilityPhase.EndAbilityPhase();
        }

        Debug.Log("Finished All Ability Phases");
    }
}
