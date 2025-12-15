using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Consequence/AreaOfEffect")]
public class AreaOfEffectConsequence : Consequence
{
    [SerializeField] private Stat<float> _areaRadius;
    [SerializeField] private Stat<float> _effectDuration;
    [SerializeField] private Stat<float> _triggerCount;
    [SerializeField] private AreaOfEffectObject _areaOffEffectPrefab;
    [SerializeField] private LayerMask _targetLayers;
    [SerializeField] private SOTransform _areaOfEffectTargetPosition; //is manualy preset based on character design

    protected override string ReceiveDataKey => "Status";

    protected override string SendDataKey => throw new System.NotImplementedException();

    protected override void ConsequenceEffect(Dictionary<string, ConsequenceAndValue> consequenceData)
    {
        if (consequenceData.TryGetValue(ReceiveDataKey, out var StatusData))
        {
            Status statusToUse = (Status)StatusData.arguments;
            AreaOfEffectObject areaOfEffectObjectInstance = Instantiate(_areaOffEffectPrefab, _areaOfEffectTargetPosition.value.position, Quaternion.identity);
            areaOfEffectObjectInstance.InitializeAreaOfEffect(statusToUse, _areaRadius.Value, _effectDuration.Value, _triggerCount.Value, _targetLayers);
        }
    }
}
