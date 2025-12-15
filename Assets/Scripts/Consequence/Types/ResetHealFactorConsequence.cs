using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Consequence/ResetFactor")]
public class ResetHealFactorConsequence : Consequence
{
    protected override string ReceiveDataKey => "Targets";

    protected override string SendDataKey => "";

    protected override void ConsequenceEffect(Dictionary<string, ConsequenceAndValue> consequenceData)
    {
        if (consequenceData.TryGetValue(ReceiveDataKey, out ConsequenceAndValue data))
        {
            Collider[] targets = (Collider[])data.arguments;

            foreach (Collider target in targets)
            {
                if (target.TryGetComponent<Health>(out Health healthComponent))
                {
                    healthComponent.ResetHealingFactor();
                }
            }
        }
    }
}
