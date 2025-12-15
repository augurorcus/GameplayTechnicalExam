using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Consequence/SphereOverlapConsequence")]
public class SphereOverlapConsequence : Consequence
{
    [SerializeField] private Stat<int> _sphereRadius;
    [SerializeField] private GameObject _visuals;
    [SerializeField] private LayerMask _targetLayer;
    protected override string ReceiveDataKey => "Position";

    protected override string SendDataKey => "Targets";

    protected override void ConsequenceEffect(Dictionary<string, ConsequenceAndValue> consequenceData)
    {
        if (consequenceData.TryGetValue(ReceiveDataKey, out ConsequenceAndValue data))
        {
            Vector3 targetPosition = (Vector3)data.arguments;
            Collider[] acquiredTargets = Physics.OverlapSphere(targetPosition, _sphereRadius.Value, _targetLayer);
            consequenceData.Add(SendDataKey, new ConsequenceAndValue(this, acquiredTargets));

            GameObject spawnedSphere = Instantiate(_visuals);
            spawnedSphere.transform.position = targetPosition;
            spawnedSphere.transform.localScale = new Vector3(_sphereRadius.Value, _sphereRadius.Value, _sphereRadius.Value);
            Destroy(spawnedSphere, 0.15f);
            Debug.Log("SphereOverlap");
        }
    }

}
