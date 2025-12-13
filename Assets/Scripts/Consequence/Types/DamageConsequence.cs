using UnityEngine;

[CreateAssetMenu(menuName = "Consequence/Damage")]
public class DamageConsequence : Consequence
{
    [SerializeField] private Stat<float> _damageValue;
    [SerializeField] private string _customDamageText;

    public Stat<float> DamageValue { get => _damageValue; }

    protected override void ConsequenceEffect()
    {
        Debug.Log("Damaged Enemy: " + _damageValue.Value + " " + _customDamageText);
    }

    protected override bool IsConsequenceFinished()
    {
        return true;
    }
}
