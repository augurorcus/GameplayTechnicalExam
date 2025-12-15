using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class AbilityPhase
{
    [SerializeField, Header("Design Details")] private string phaseName;
    [SerializeField, TextArea] private string phaseDescription;

    [SerializeField] private Stat<float> _phaseDuration;
    [SerializeField] private ConsequenceModules _consequenceModules;

    private float _elapsedTime;
    private List<float> _listOfNormalizedTime;

    Dictionary<string, ConsequenceAndValue> consequenceData;

    public void StartAbilityPhase()
    {
        Debug.Log("Start Phase:" +  phaseName);
        _listOfNormalizedTime = new List<float>();
        _listOfNormalizedTime = _consequenceModules.ConsequenceValues.Keys.ToList();
        _elapsedTime = 0;
        consequenceData = new Dictionary<string, ConsequenceAndValue>();
    }

    public async UniTask ProcessConsequences()
    {
        Debug.Log("Start Process: " + phaseName);
        while (_elapsedTime < _phaseDuration.Value)
        {

            _elapsedTime += Time.deltaTime;
            float currentNormalizedTime = Mathf.Clamp01(_elapsedTime / _phaseDuration.Value);
            float adjustedurrentNormalizedTime =(float)Math.Round(currentNormalizedTime, 2);
            int? indexToRemove = null;


            for (int timeLookUpIndex = 0; timeLookUpIndex < _listOfNormalizedTime.Count; timeLookUpIndex++)
            {
                float normalizedTimeChecked = _listOfNormalizedTime[timeLookUpIndex];

                if (normalizedTimeChecked <= adjustedurrentNormalizedTime)
                {
                    indexToRemove = timeLookUpIndex;

                    if (_consequenceModules.ConsequenceValues.TryGetValue(normalizedTimeChecked, out List<Consequence> consequenceExecuteList))
                    {
                        Debug.Log("Found a Consequence");
                        foreach (Consequence currentConsequence in consequenceExecuteList)
                        {
                            currentConsequence.TriggerConsequence(consequenceData);
                            //ConsequenceHandler.Instance.RegisterConsequenceCommand(currentConsequence.TriggerConsequence);
                        }
                    }
                }
            }

            if(indexToRemove != null)
            {
                _listOfNormalizedTime.RemoveAt(indexToRemove.Value);
            }

            await UniTask.Yield();
        }
    }

    public void EndAbilityPhase()
    {
        Debug.Log("End Phase:" + phaseName);
        _elapsedTime = 0;
    }
}
