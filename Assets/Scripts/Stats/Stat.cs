using Sirenix.OdinInspector;
using System;
using System.Xml;
using UniRx;
using UnityEngine;

[Serializable]
public class Stat<T>
{
    [SerializeField] private T value;
    [SerializeField] private string _statID;

    [Button()]
    public void UpdateStatID()
    {
        string uniqueID = Guid.NewGuid().ToString();
        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        long uniqueNumber = long.Parse(timestamp);

        _statID = "ID" + uniqueID + uniqueNumber.ToString().Substring(0, 4);
    } 

    public T Value { get => value; }

}
