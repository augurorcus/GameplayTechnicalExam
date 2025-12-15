using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Consequence/Death")]
public class DeathConsequence : Consequence
{
    [SerializeField] private Stat<float> _delayBeforeDeath;
    protected override string ReceiveDataKey => "Targets";

    protected override string SendDataKey => "";

    protected override void ConsequenceEffect(Dictionary<string, ConsequenceAndValue> consequenceData)
    {
        if (consequenceData.TryGetValue(ReceiveDataKey, out ConsequenceAndValue data))
        {
            Collider[] targets = (Collider[])data.arguments;

            foreach (Collider currentTarget in targets)
            {
                Destroy(currentTarget.gameObject, _delayBeforeDeath.Value);
            }
        }
    }
}
