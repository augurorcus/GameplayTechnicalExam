using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Consequence/AreaOfEffect")]
public class AreaOfEffectConsequence : Consequence
{
    [SerializeField] private Stat<float> _areaRadius;
    [SerializeField] private Stat<float> _effectDuration;
    [SerializeField] private Stat<int> _triggerCount;
    [SerializeField] private AreaOfEffectObject _areaOffEffectPrefab;
    [SerializeField] private LayerMask _targetLayers;
    [SerializeField] private SOTransform _areaOfEffectTargetPosition; //is manualy preset based on character design
    [SerializeField] private List<Consequence> _consequences;

    protected override string ReceiveDataKey => "";

    protected override string SendDataKey => "";

    protected override void ConsequenceEffect(Dictionary<string, ConsequenceAndValue> consequenceData)
    {
        AreaOfEffectObject areaOfEffectObjectInstance = Instantiate(_areaOffEffectPrefab, _areaOfEffectTargetPosition.value.position, Quaternion.identity);
        areaOfEffectObjectInstance.InitializeAreaOfEffect( _areaRadius.Value, _effectDuration.Value, _triggerCount.Value, _targetLayers, _consequences);
        Debug.Log("AOE Executed");
    }
}
