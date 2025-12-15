using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Test.Core.Utilities;
using UnityEngine;

[CreateAssetMenu()]
public class Status : ScriptableObject
{
    public bool IsStackable;
    public int MaxStack;
    public bool ClearWholeStackOnFinish;
    public bool IsRefreshable;
    public bool CanDoMultipleInstance; //can do multiple instance with different stacks
    public List<Consequence> ConsequencesToTrigger;
    public List<Consequence> ConsequencesOnExpired;
}

public class StatusInstance
{
    private Status _statusType;
    private int _currentTotalStack;
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

    public StatusInstance(Status statusType, float totalDuration, int triggerCount)
    {
        _totalTriggerCount = triggerCount;
        _totalDuration = totalDuration;
        _statusType = statusType;
        _cooldownTimer = new CountDownTimer(totalDuration, 0, 1);
        _frequency = totalDuration / triggerCount;
        _currentTriggerCount = 0;
        _currentFrequency = 0;
        consequenceData = new Dictionary<string, ConsequenceAndValue>();
        _statusHasExpired = false;
    }

    public void ResetStatus()
    {
        _statusHasExpired = false;
    }

    public void RefreshStatus()
    {
        if (!_statusType.IsRefreshable) return;
        consequenceData.Clear();
        _currentTriggerCount = 0;
        _currentFrequency = 0;
        _cooldownTimer.Restart();
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

            _cooldownTimer.Tick(Time.deltaTime);
        }
        else
        {
            if (_currentTotalStack <= 0 && _currentTriggerCount < _totalTriggerCount)
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
                _currentTotalStack -= 1;
                RefreshStatus();
            }
        }
    }

    private void TriggerConsequences(Character character)
    {
        consequenceData.Clear();

        consequenceData.Add("Character", new ConsequenceAndValue(null, character));

        for (int consequenceIndex = 0; consequenceIndex < _statusType.ConsequencesToTrigger.Count; consequenceIndex++)
        {
            Consequence currentConsequence = _statusType.ConsequencesToTrigger[consequenceIndex];
            currentConsequence.TriggerConsequence(consequenceData);
        }
    }

    private void TriggerConsequencesOnExipred()
    {
        consequenceData.Clear();

        for (int consequenceIndex = 0; consequenceIndex < _statusType.ConsequencesOnExpired.Count; consequenceIndex++)
        {
            Consequence currentConsequence = _statusType.ConsequencesOnExpired[consequenceIndex];
            currentConsequence.TriggerConsequence(consequenceData);
        }
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
        _currentTotalStack += Mathf.Abs(value);
    }

    public void SubtractStack(int value)
    {
        _currentTotalStack -= Mathf.Abs(value);
    }

    public Status StatusType { get => _statusType; }
}
