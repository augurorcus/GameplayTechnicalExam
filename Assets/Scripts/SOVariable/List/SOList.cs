using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SOList<T> : ScriptableObject, ISerializationCallbackReceiver
{
    public List<T> list;
    [System.NonSerialized] public List<T> runtimeList;

    public void OnAfterDeserialize()
    {
        runtimeList = list;
    }

    public void OnBeforeSerialize(){ }
}
