using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Consequence/SphereOverlapConsequence")]
public class SphereOverlapConsequence : Consequence
{
    private Stat<int> _sphereRadius;

    protected override string ReceiveDataKey => "Position";

    protected override string SendDataKey => "Targets";

    protected override void ConsequenceEffect(Dictionary<string, ConsequenceAndValue> consequenceData)
    {
        if (consequenceData.TryGetValue(ReceiveDataKey, out ConsequenceAndValue data))
        {
            Vector3 targetPosition = (Vector3)data.arguments;
            Collider[] acquiredTargets = Physics.OverlapSphere(targetPosition, _sphereRadius.Value);
            consequenceData.Add(SendDataKey, new ConsequenceAndValue(this, acquiredTargets));
        }
    }

}
