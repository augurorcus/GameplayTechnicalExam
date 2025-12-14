using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Consequence/RectOverlap")]
public class RectOverlapConsequence : Consequence
{
    [SerializeField] private Stat<float> _rectWidth;
    [SerializeField] private Stat<float> _rectDepth;
    [SerializeField] private Stat<float> _rectHeight;

    protected override string ReceiveDataKey => "Position";

    protected override string SendDataKey => "Targets";

    protected override void ConsequenceEffect(Dictionary<string, ConsequenceAndValue> consequenceData)
    {
        if (consequenceData.TryGetValue(ReceiveDataKey, out ConsequenceAndValue value))
        {
            Vector3 centerPosition = (Vector3)value.arguments;
            Collider[] overlappedObjects = Physics.OverlapBox(centerPosition, new Vector3(_rectWidth.Value, _rectHeight.Value, _rectDepth.Value));
            consequenceData.Add(SendDataKey, new ConsequenceAndValue(this, overlappedObjects));
        }
    }
}
