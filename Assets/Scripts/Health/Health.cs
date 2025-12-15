using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class Health : MonoBehaviour, IDamageable, IHealable
{
    [SerializeField] private FloatVariable _maxHealth;
    [SerializeField,ReadOnly] private float _currentHealth;

    [SerializeField] private Consequence _OnDeathConsequence;

    public UnityEvent<float> OnHealthUpdate;
    public UnityEvent<float> OnDeath;

    public void Damage(float value)
    {
        float temporaryHealth = _currentHealth;
        temporaryHealth -= Mathf.Abs(value);

        if (temporaryHealth <= 0)
        {
            _currentHealth = 0;
        }
        else
        {
            _currentHealth = temporaryHealth;   
        }

        OnHealthUpdate.Invoke(_currentHealth);

        if(_currentHealth <= 0)
        {
            Dictionary<string, ConsequenceAndValue> data = new Dictionary<string, ConsequenceAndValue>();
            _OnDeathConsequence.TriggerConsequence(data);
        }
    }

    public void Heal(float value)
    {
        float temporaryHealth = _currentHealth;
        temporaryHealth += Mathf.Abs(value);

        if(temporaryHealth >= _maxHealth.Value)
        {
            _currentHealth = _maxHealth.Value;
        }
        else
        {
            _currentHealth = temporaryHealth;
        }

        OnHealthUpdate.Invoke(_currentHealth);
    }
}

public interface IDamageable
{
    void Damage(float value);
}

public interface IHealable
{
    void Heal(float value);
}