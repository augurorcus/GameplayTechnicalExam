using Cysharp.Threading.Tasks;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Events;
using Test.Core.Utilities;

[CreateAssetMenu()]
public class Ability : ScriptableObject
{
    [SerializeField] private string _abilityID;
    [SerializeField] private FloatVariable _globalCooldown;
    [SerializeField] private AbilityBehavior _abilityBehavior;

    private CooldownHandler _abilityCooldownHandler;

    public UnityEvent<float> OnStartAbility;
    public CooldownHandler AbilityCooldownHandler { get => _abilityCooldownHandler; }

    public void InitializeAbility()
    {
        _abilityCooldownHandler = new CooldownHandler(_globalCooldown.Value);
    }

    public async UniTask TriggerAbilityBehavior()
    {
        if (!_abilityCooldownHandler.IsOnCooldown)
        {
            OnStartAbility.Invoke(1);
            await _abilityBehavior.ExecuteAbilityBehavior();
            await _abilityCooldownHandler.StartCooldown();
        }
    }
}

[System.Serializable]
public class CooldownHandler
{
    public float _cooldownDuration;

    public bool IsOnCooldown { get => _timeElapsed < _cooldownDuration; }

    private float _timeElapsed = 0f;

    public UnityEvent<float> OnStartTimer;
    public UnityEvent<float> OnTickTimer;
    public UnityEvent<float> OnEndTimer;
    private float _normalizedTime;
    public CooldownHandler(float cooldownDuration)
    {
        _cooldownDuration = cooldownDuration;
        _timeElapsed = _cooldownDuration;
        OnStartTimer = new UnityEvent<float>();
        OnTickTimer = new UnityEvent<float>();
        OnEndTimer = new UnityEvent<float>();
    }
    public async UniTask StartCooldown()
    {
        OnStartTimer.Invoke(1);
         _timeElapsed = 0f;
        while (_timeElapsed < _cooldownDuration)
        {
            _normalizedTime = Helper.Normalize(_timeElapsed, 0, _cooldownDuration);
            OnTickTimer.Invoke(1 - _normalizedTime);
            _timeElapsed += Time.deltaTime;
            await UniTask.Yield();
        }
        OnEndTimer.Invoke(0);
    }
}
