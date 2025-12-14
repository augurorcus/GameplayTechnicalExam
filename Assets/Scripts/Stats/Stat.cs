using System;
using UnityEngine;
using Sirenix.OdinInspector;

[Serializable]
public class Stat<T>
{
    [SerializeField] private T value;
    [SerializeField] private string _statID;

    [Button()]
    public void UpdateStatID(T value)
    {
        _statID = "ID"+ DateTime.Today.ToString("yyyyMMdd").GetHashCode();
    } 

    public T Value { get => value; }

}
