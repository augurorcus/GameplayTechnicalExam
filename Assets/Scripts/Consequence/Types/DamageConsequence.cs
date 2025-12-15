using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Consequence/Damage")]
public class DamageConsequence : Consequence
{
    [SerializeField] private Stat<float> _damageValue;
    [SerializeField] private string _customDamageText;

    private List<IDamageable> _targetsToDamage;

    public Stat<float> DamageValue { get => _damageValue; }

    protected override string ReceiveDataKey => "Targets";

    protected override string SendDataKey => "Result";

    protected override void ConsequenceEffect(Dictionary<string, ConsequenceAndValue> consequenceData)
    {
        _targetsToDamage = new List<IDamageable>();

        if(consequenceData.TryGetValue(ReceiveDataKey, out ConsequenceAndValue data))
        {
            Collider[] targets = (Collider[])data.arguments;

            foreach (Collider target in targets) 
            {
                if (target.TryGetComponent<IDamageable>(out IDamageable damageComponent))
                {
                    _targetsToDamage.Add(damageComponent);
                }
            }
        }

        if (_targetsToDamage.Count > 0)
        {
            foreach (IDamageable currentTarget in _targetsToDamage)
            {
                currentTarget.Damage(_damageValue.Value);
            }

            Debug.Log("Damaged Enemy: " + _damageValue.Value + " " + _customDamageText);
        }

    }
}
