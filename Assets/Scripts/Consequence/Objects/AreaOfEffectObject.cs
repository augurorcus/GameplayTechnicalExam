using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Test.Core.Utilities;


public class AreaOfEffectObject : MonoBehaviour
{
    private float _areaRadius;
    private float _effectFrequency;
    private int _totalTriggers;
    private Timer _effectTimer;
    private LayerMask _targetLayerMask;

    private float _currentFrequency;
    private int _currentTriggerCount;

    public void InitializeAreaOfEffect(Status statusToUse, float radius, float duration, float triggerCount, LayerMask targetLayers)
    {
        _effectTimer = new CountDownTimer(duration, 0, 1);
        _effectFrequency = duration / triggerCount;
        _targetLayerMask = targetLayers;
        _areaRadius = radius;
    }

    private void Update()
    {
        if (_currentTriggerCount < _totalTriggers)
        {
            if (!_effectTimer.IsFinished())
            {
                _currentFrequency += Time.deltaTime;

                if (_currentFrequency >= _effectFrequency)
                {
                    _currentFrequency = 0;
                    CheckAreaAndTrigger();
                    _currentTriggerCount++;
                }

                _effectTimer.Tick(Time.deltaTime);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CheckAreaAndTrigger()
    {
        Collider[] acquiredTargets = Physics.OverlapSphere(transform.position, _areaRadius, _targetLayerMask);
    }
}
