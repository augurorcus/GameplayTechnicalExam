using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterStatusHandler : CharacterComponent, IStatusApplicable
{
    [SerializeField] private StatusFactory _statusFactory;
    [SerializeField, ReadOnly] private List<StatusInstance> _currentActiveStatus;

    private void Start()
    {
        _currentActiveStatus = new List<StatusInstance>();
    }

    public void AddStatus(Status statusToAdd, float duration, int triggerCount)
    {
        StatusInstance existingStatusInstance = _currentActiveStatus.FirstOrDefault(x => x.StatusType == statusToAdd);

        if (existingStatusInstance != null && !existingStatusInstance.IsStackFull())
        {
            //if there is an existing status
            if (existingStatusInstance.StatusType.IsStackable && !existingStatusInstance.IsStackFull())
            {
                existingStatusInstance.AddStack(1);
                if (existingStatusInstance.StatusType.IsRefreshable)
                {
                    existingStatusInstance.RefreshStatus();
                }
            }
            else
            {
                if (existingStatusInstance.StatusType.CanDoMultipleInstance)
                    AddNewStatus(statusToAdd, duration, triggerCount);
            }
        }
        else
        {
            AddNewStatus(statusToAdd, duration, triggerCount);
        }
    }

    private void AddNewStatus(Status statusToAdd, float duration, int triggerCount)
    {

        StatusInstance newStatusToAdd = _statusFactory.CreateProduct(statusToAdd, duration, triggerCount);
        _currentActiveStatus.Add(newStatusToAdd);
    }

    private void Update()
    {
        if (_currentActiveStatus.Count <= 0) return;

        foreach (StatusInstance statusInstance in _currentActiveStatus)
        {
            statusInstance.ProcessStatus(entity);
        }
    }

    private void LateUpdate()
    {
        if (_currentActiveStatus.Count <= 0) return;

        for (int statusIndex = _currentActiveStatus.Count - 1; statusIndex >= 0; statusIndex--)
        {
            StatusInstance currentStatus = _currentActiveStatus[statusIndex];

            if (currentStatus.IsStatusFinished())
            {
                if (!currentStatus.StatusType.ClearWholeStackOnFinish)
                {
                    currentStatus.SubtractStack(1);
                }
                else
                {
                    _currentActiveStatus.RemoveAt(statusIndex);
                }
            }
        }
    }
}

public interface IStatusApplicable
{
    public void AddStatus(Status statusToAdd, float duration, int triggerCount);
}
