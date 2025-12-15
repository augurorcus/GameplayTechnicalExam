using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Consequence/StatusInput")]
public class StatusInputConsequence : Consequence
{
    [SerializeField] private Status _statusToPass;

    protected override string ReceiveDataKey => "";

    protected override string SendDataKey => "Status";

    protected override void ConsequenceEffect(Dictionary<string, ConsequenceAndValue> consequenceData)
    {
        consequenceData.Add(SendDataKey, new ConsequenceAndValue(this, _statusToPass));
    }
}
