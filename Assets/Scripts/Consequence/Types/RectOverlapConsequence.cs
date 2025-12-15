using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Consequence/RectOverlap")]
public class RectOverlapConsequence : Consequence
{
    [SerializeField] private Stat<float> _rectWidth;
    [SerializeField] private Stat<float> _rectDepth;
    [SerializeField] private Stat<float> _rectHeight;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject _visuals;

    protected override string ReceiveDataKey => "Position";

    protected override string SendDataKey => "Targets";

    protected override void ConsequenceEffect(Dictionary<string, ConsequenceAndValue> consequenceData)
    {
        if (consequenceData.TryGetValue(ReceiveDataKey, out ConsequenceAndValue value))
        {
            Vector3 centerPosition = (Vector3)value.arguments;
            GameObject spawnedRect = Instantiate(_visuals);
            spawnedRect.transform.position = centerPosition;
            spawnedRect.transform.localScale = new Vector3(_rectWidth.Value, _rectHeight.Value, _rectDepth.Value);
            Destroy(spawnedRect, 0.25f);
            Collider[] overlappedObjects = Physics.OverlapBox(centerPosition, new Vector3(_rectWidth.Value, _rectHeight.Value, _rectDepth.Value), Quaternion.identity,_layerMask);
            consequenceData.Add(SendDataKey, new ConsequenceAndValue(this, overlappedObjects));
            Debug.Log("RectOverlap");
        }
    }
}
