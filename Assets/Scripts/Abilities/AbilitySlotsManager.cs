using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public class AbilitySlotsManager : MonoBehaviour
{
    [SerializeField] private AbilitySlots _abilitySlotsPrefab;

    [SerializeField] private SOListAbility _characterAbilityList;



    //public async UniTask Start()
    //{
    //    Debug.Log("start");
    //    abilityToTrigger.InitializeAbility();
    //}

    //[Button()]
    //public async UniTask TriggerAbility()
    //{
    //    await abilityToTrigger.TriggerAbilityBehavior();
    //}
}
