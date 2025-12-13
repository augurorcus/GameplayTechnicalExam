using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public class AbilitySlotsManager : MonoBehaviour
{
    [SerializeField] private Ability abilityToTrigger;

    public async UniTask Start()
    {
        Debug.Log("start");
        abilityToTrigger.InitializeAbility();
    }

    [Button()]
    public async UniTask TriggeAbility()
    {
        await abilityToTrigger.TriggerAbilityBehavior();
    }
}
