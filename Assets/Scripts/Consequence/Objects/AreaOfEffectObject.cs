using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Test.Core.Utilities;
using UnityEngine.Experimental.Rendering;


public class AreaOfEffectObject : MonoBehaviour
{
    [SerializeField] private Transform _visuals;

    private float _areaRadius;
    private float _effectFrequency;
    private int _totalTriggers;
    private Timer _effectTimer;
    private LayerMask _targetLayerMask;

    private float _currentFrequency;
    private int _currentTriggerCount;

    private List<Consequence> _consequences;
    Dictionary<string, ConsequenceAndValue> _consequenceData;

    public void InitializeAreaOfEffect( float radius, float duration, int triggerCount, LayerMask targetLayers, List<Consequence> consequences)
    {
        _effectTimer = new CountDownTimer(duration, 0, 1);
        _effectTimer.Restart();
        _effectFrequency = duration / triggerCount;
        _targetLayerMask = targetLayers;
        _areaRadius = radius;
        _visuals.transform.localScale = new Vector3(radius, _visuals.transform.localScale.y, radius);
        _consequences = consequences;
        _totalTriggers = triggerCount;
        _currentTriggerCount = 0;
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
                    TriggerConsequences();
                    _currentTriggerCount++;
                }

                _effectTimer.Tick(Time.deltaTime);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TriggerConsequences()
    {
        _consequenceData = new Dictionary<string, ConsequenceAndValue>();

        Collider[] acquiredTargets = Physics.OverlapSphere(transform.position, _areaRadius, _targetLayerMask);

        _consequenceData.Add("Targets", new ConsequenceAndValue(null, acquiredTargets));

        foreach (Consequence currentConsequence in _consequences)
        {
            currentConsequence.TriggerConsequence(_consequenceData);
        }

        Debug.Log("AOE Trigger");
    }
}
