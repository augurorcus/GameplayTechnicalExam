using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

[CreateAssetMenu(menuName ="Factories/Status")]
public class StatusFactory : FactoryBase<StatusInstance>
{
    [SerializeField] private Status _defaultStatus;
    [SerializeField] private FloatVariable _defaultStatusDuration;
    [SerializeField] private IntVariable _defaultStatusTriggerCount;


    public StatusInstance CreateProduct(Status status, float duration, int triggerCount)
    {
        StatusInstance newActiveStatus = new StatusInstance(status,duration, triggerCount);
        return newActiveStatus;
    }

    public override StatusInstance CreateProduct()
    {
        StatusInstance newActiveStatus = new StatusInstance(_defaultStatus, _defaultStatusDuration.Value, _defaultStatusTriggerCount.Value);
        return newActiveStatus;
    }
}
