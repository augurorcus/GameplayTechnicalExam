using Cysharp.Threading.Tasks;
using UnityAtoms.BaseAtoms;
using UnityEngine;

[CreateAssetMenu()]
public class Ability : ScriptableObject
{
    [SerializeField] private string _abilityID;
    [SerializeField] private FloatVariable _globalCooldown;
    [SerializeField] private AbilityBehavior _abilityBehavior;

    private CooldownHandler _abilityCooldownHandler;

    public void InitializeAbility()
    {
        _abilityCooldownHandler = new CooldownHandler(_globalCooldown.Value);
    }

    public async UniTask TriggerAbilityBehavior()
    {
        if (!_abilityCooldownHandler.IsOnCooldown)
        {
            await _abilityBehavior.ExecuteAbilityBehavior();
            await _abilityCooldownHandler.StartCooldown();
        }
    }
}

public class CooldownHandler
{
    public float _cooldownDuration;

    public bool IsOnCooldown { get => _timeElapsed < _cooldownDuration; }

    private float _timeElapsed = 0f;

    public CooldownHandler(float cooldownDuration)
    {
        _cooldownDuration = cooldownDuration;
        _timeElapsed = _cooldownDuration;
    }
    public async UniTask StartCooldown()
    {
         _timeElapsed = 0f;
        while (_timeElapsed < _cooldownDuration)
        {
            _timeElapsed += Time.deltaTime;
            await UniTask.Yield();
        }
    }
}
