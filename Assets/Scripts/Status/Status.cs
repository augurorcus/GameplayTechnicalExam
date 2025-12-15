using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Test.Core.Utilities;
using UnityEngine;

[CreateAssetMenu()]
public class Status : ScriptableObject
{
    public bool IsStackable;
    public int MaxStack;
    public bool IsRefreshable;
    public bool RemoveAllStacksOnExpire;
    public bool CanDoMultipleInstance; //can do multiple instance with different stacks
    public List<Consequence> ConsequencesOnStart;
    public List<Consequence> ConsequencesToTriggerOnFrequency;
    public List<Consequence> ConsequencesToTriggerOnTick;
    public List<Consequence> ConsequencesOnExpired;
}

[Serializable]
public class StatusInstance
{
    [SerializeField, ReadOnly] private Status _statusType;
    [SerializeField, ReadOnly] private int _currentTotalStack;
    private int _currentTriggerCount;
    private float _frequency;
    private Timer _cooldownTimer;
    private bool _isStatusFinished;
    private float _totalDuration;
    private int _totalTriggerCount;
    private float _currentFrequency;
    private bool _statusHasExpired;

    Dictionary<string, ConsequenceAndValue> consequenceData;
    private Consequence _consequenceToTrigger;
    private CharacterStatusHandler _target;

    public StatusInstance(Status statusType, float totalDuration, int triggerCount)
    {
        float overridenDuration = totalDuration;
        int overridenTriggerCount = triggerCount;
        _totalTriggerCount = overridenTriggerCount;
        _totalDuration = overridenDuration;
        _statusType = statusType;
        _cooldownTimer = new CountDownTimer(overridenDuration, 0, 1);
        _cooldownTimer.Restart();
        _frequency = overridenDuration / overridenTriggerCount;
        _currentTriggerCount = 0;
        _currentFrequency = 0;
        consequenceData = new Dictionary<string, ConsequenceAndValue>();
        _statusHasExpired = false;
        _currentTotalStack = 1;
    }

    public void SetTarget(CharacterStatusHandler target)
    {
        _target = target;
        TriggerConsequencesOnStart();
    }

    public void RefreshStatus()
    {
        if (!_statusType.IsRefreshable) return;
        consequenceData.Clear();
        _currentTriggerCount = 0;
        _currentFrequency = 0;
        _cooldownTimer.Restart();
         TriggerConsequencesOnStart();
    }

    public void ProcessStatus(Character character)
    {
        if (_isStatusFinished) return;

        if (!_cooldownTimer.IsFinished())
        {
            _currentFrequency += Time.deltaTime;

            if (_currentFrequency >= _frequency)
            {
                TriggerConsequences(character);
                _currentFrequency = 0;
                _currentTriggerCount++;
            }

            TriggerConsequencesOnTick();

            _cooldownTimer.Tick(Time.deltaTime);
        }
        else
        {
            if (_currentTotalStack <= 0 && _currentTriggerCount >= _totalTriggerCount)
            {
                if (!_statusHasExpired)
                {
                    TriggerConsequencesOnExipred();
                    _statusHasExpired = true;
                }
                _isStatusFinished = true;
            }
            else
            {
                if (!_statusType.RemoveAllStacksOnExpire)
                {
                    SubtractStack(1);
                    RefreshStatus();
                }
                else
                {
                    if (!_statusHasExpired)
                    {
                        TriggerConsequencesOnExipred();
                        _statusHasExpired = true;
                    }
                    _isStatusFinished = true;
                }
            }
        }
    }

    private void TriggerConsequences(Character character)
    {
        if (_statusType.ConsequencesToTriggerOnFrequency.Count <= 0) return;

        consequenceData.Clear();

        consequenceData.Add("Character", new ConsequenceAndValue(null, character));
        Collider[] newTargets = new Collider[] { _target.gameObject.GetComponent<Collider>() };
        consequenceData.Add("Targets", new ConsequenceAndValue(null, newTargets));

        for (int consequenceIndex = 0; consequenceIndex < _statusType.ConsequencesToTriggerOnFrequency.Count; consequenceIndex++)
        {
            Consequence currentConsequence = _statusType.ConsequencesToTriggerOnFrequency[consequenceIndex];
            currentConsequence.TriggerConsequence(consequenceData);
        }

        Debug.Log("Status Trigger On Frequency");
    }

    private void TriggerConsequencesOnTick()
    {
        if (_statusType.ConsequencesToTriggerOnTick.Count <= 0) return;

        consequenceData.Clear();

         Collider[] newTargets = new Collider[] { _target.gameObject.GetComponent<Collider>() };
        consequenceData.Add("Targets", new ConsequenceAndValue(null, newTargets));

        for (int consequenceIndex = 0; consequenceIndex < _statusType.ConsequencesToTriggerOnTick.Count; consequenceIndex++)
        {
            Consequence currentConsequence = _statusType.ConsequencesToTriggerOnTick[consequenceIndex];
            currentConsequence.TriggerConsequence(consequenceData);
        }

        Debug.Log("Status Trigger On Tick");
    }
    private void TriggerConsequencesOnStart()
    {
        if (_statusType.ConsequencesOnStart.Count <= 0) return;
        consequenceData.Clear();

        Collider[] newTargets = new Collider[] { _target.gameObject.GetComponent<Collider>() };
        consequenceData.Add("Targets", new ConsequenceAndValue(null, newTargets));

        for (int consequenceIndex = 0; consequenceIndex < _statusType.ConsequencesOnStart.Count; consequenceIndex++)
        {
            Consequence currentConsequence = _statusType.ConsequencesOnStart[consequenceIndex];
            currentConsequence.TriggerConsequence(consequenceData);
        }

        Debug.Log("Status Trigger On Start");
    }

    private void TriggerConsequencesOnExipred()
    {
        if (_statusType.ConsequencesOnExpired.Count <= 0) return;
        consequenceData.Clear();

        Collider[] newTargets = new Collider[] { _target.gameObject.GetComponent<Collider>() };
        consequenceData.Add("Targets", new ConsequenceAndValue(null, newTargets));

        for (int consequenceIndex = 0; consequenceIndex < _statusType.ConsequencesOnExpired.Count; consequenceIndex++)
        {
            Consequence currentConsequence = _statusType.ConsequencesOnExpired[consequenceIndex];
            currentConsequence.TriggerConsequence(consequenceData);
        }


        Debug.Log("Status Trigger On Expired");
    }

    public bool IsStatusFinished()
    {
        return _isStatusFinished;
    }

    public bool IsStackFull()
    {
        return _currentTotalStack >= _statusType.MaxStack;
    }

    public void AddStack(int value)
    {
        _currentTotalStack = Mathf.Clamp(_currentTotalStack + Mathf.Abs(value), 0, StatusType.MaxStack);
    }

    public void SubtractStack(int value)
    {
        _currentTotalStack = Mathf.Clamp(_currentTotalStack - Mathf.Abs(value), 0, StatusType.MaxStack);
    }

    public Status StatusType { get => _statusType; }
}
