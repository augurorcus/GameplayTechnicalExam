using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Consequence/HealTarget")]
public class HealTargetConsequence : Consequence
{
    [SerializeField] private Stat<float> _healValue;

    protected override string ReceiveDataKey => "Targets";

    protected override string SendDataKey => "";

    protected override void ConsequenceEffect(Dictionary<string, ConsequenceAndValue> consequenceData)
    {
        if (consequenceData.TryGetValue(ReceiveDataKey, out ConsequenceAndValue data))
        {
            Collider[] targets = (Collider[])data.arguments;

            foreach (Collider currentTarget in targets)
            {
                if (currentTarget.TryGetComponent<IHealable>(out IHealable healComponent))
                {
                    healComponent.Heal(_healValue.Value);
                }
            }
        }
    }
}
