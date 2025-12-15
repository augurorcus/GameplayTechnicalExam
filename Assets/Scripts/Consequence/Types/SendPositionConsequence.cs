using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Consequence/SendPosition")]
public class SendPositionConsequence : Consequence
{
    [SerializeField] private SOTransform _targetTransform;

    protected override string ReceiveDataKey => "";

    protected override string SendDataKey => "Position";

    protected override void ConsequenceEffect(Dictionary<string, ConsequenceAndValue> consequenceData)
    {
        consequenceData.Add(SendDataKey, new ConsequenceAndValue(this, _targetTransform.value.position));
        Debug.Log("Sent Position");
    }
}
