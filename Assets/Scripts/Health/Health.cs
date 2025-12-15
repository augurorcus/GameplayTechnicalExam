using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class Health : MonoBehaviour, IDamageable, IHealable
{
    [SerializeField] private FloatVariable _maxHealth;
    [SerializeField,ReadOnly] private float _currentHealth;

    [SerializeField] private List<Consequence> _OnDeathConsequence;
    
    public UnityEvent<float> OnHealthUpdate;
    public UnityEvent<float> OnDeath;

    [SerializeField] private int _maxHealFactor;
    private int _healFactor = 0;

    private void Start()
    {
        _currentHealth = _maxHealth.Value;
    }

    public void IncreaseHealingFactor(int healFactorToAdd)
    {
        if (_healFactor >= _maxHealFactor) return;

        _healFactor += Mathf.Abs(healFactorToAdd);
    }

    public void ResetHealingFactor()
    {
        _healFactor = 0;
    }

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

        if (_currentHealth <= 0)
        {
            Debug.Log("die");
            Dictionary<string, ConsequenceAndValue> data = new Dictionary<string, ConsequenceAndValue>();

            Collider[] newTargets = new Collider[] { gameObject.GetComponent<Collider>() };
            data.Add("Targets", new ConsequenceAndValue(null, newTargets));

            foreach (Consequence currentConsequence in _OnDeathConsequence)
            {
                currentConsequence.TriggerConsequence(data);
            }
        }
    }

    public void Heal(float value)
    {
        float additionalHealing = 0;

        if(_healFactor > 0)
        {
            additionalHealing = value + (value / _healFactor * 0.5f);
        }

        float temporaryHealth = _currentHealth;
        temporaryHealth += Mathf.Abs(value + additionalHealing);

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