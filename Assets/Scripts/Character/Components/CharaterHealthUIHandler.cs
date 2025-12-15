using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class CharaterHealthUIHandler : CharacterComponent
{
    [SerializeField] private FloatVariable _maxHealth;
    [SerializeField] private HealthUI _healthUIPrefab;
    
    private void Start()
    {
        Health _characterHealth = GetComponent<Health>();
        HealthUI spawnedHealthUI = Instantiate(_healthUIPrefab, entity.Visuals);
        _characterHealth.OnHealthUpdate.AddListener(spawnedHealthUI.UpdateHealthUI);
    }
}
