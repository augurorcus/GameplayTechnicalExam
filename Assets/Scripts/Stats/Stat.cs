using System;
using UnityEngine;

[Serializable]
public class Stat<T>
{
    [SerializeField] private T value;

    public T Value { get => value; }

}
