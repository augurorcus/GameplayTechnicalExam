using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Consequence/ApplyStatus")]
public class ApplyStatusConsequence : Consequence
{
    [SerializeField] private Status _statusToUse;
    [SerializeField] private Stat<int> _statusDuration;
    [SerializeField] private Stat<int> _totalTriggers;

    private Collider[] _acquiredTargets;
    protected override string ReceiveDataKey => "Targets";

    protected override string SendDataKey => "";

    protected override void ConsequenceEffect(Dictionary<string, ConsequenceAndValue> consequenceData)
    {
        if (consequenceData.TryGetValue("Targets", out var data))
        {
            _acquiredTargets = (Collider[])data.arguments;

            foreach (Collider currentTarget in _acquiredTargets)
            {
                if (currentTarget.TryGetComponent<IStatusApplicable>(out IStatusApplicable targetStatusHandler))
                {
                    targetStatusHandler.AddStatus(_statusToUse, _statusDuration.Value, _totalTriggers.Value);
                }   
            }
        }
    }
}
