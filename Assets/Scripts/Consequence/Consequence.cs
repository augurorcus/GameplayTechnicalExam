using System.Collections.Generic;
using UnityEngine;
using Cysharp;

public abstract class Consequence : ScriptableObject
{

    public void TriggerConsequence(Dictionary<string, ConsequenceAndValue> consequenceData)
    {
        if (consequenceData.ContainsKey(SendDataKey))
        {
            //override the existing send data from
            consequenceData.Remove(SendDataKey);
        }

        ConsequenceEffect(consequenceData);
    }

    protected abstract void ConsequenceEffect(Dictionary<string, ConsequenceAndValue> consequenceData);
    protected abstract string ReceiveDataKey { get; }
    protected abstract string SendDataKey { get; }
}

public class ConsequenceAndValue
{
    public Consequence consequenceObject;
    public object arguments;

    public ConsequenceAndValue(Consequence consequenceObject, object arguments)
    {
        this.consequenceObject = consequenceObject;
        this.arguments = arguments;
    }
}

public class ConsequenceCommand
{

}