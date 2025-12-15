using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ConsequenceModules : SerializedScriptableObject
{
    public Dictionary<float, List<Consequence>> ConsequenceValues;
}

