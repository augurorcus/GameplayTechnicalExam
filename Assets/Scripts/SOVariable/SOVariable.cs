using System;
using UnityEngine;
using Sirenix.OdinInspector;

public abstract class SOVariable<T> : ScriptableObject, IStringReturnable, IIntReturnable, IFloatReturnable
{
    public T value;
    
    public abstract void ResetValue();
    public abstract int ReturnValueInt();
    public abstract string ReturnValueString();
    public abstract float ReturnValueFloat();
}

public interface IIntReturnable { int ReturnValueInt(); }
public interface IStringReturnable { string ReturnValueString(); }
public interface IFloatReturnable { float ReturnValueFloat(); }
public interface ITriggerReturnable { Action returnTrigger(); }

[Serializable]
public class AnimReference<T>
{
    public string nameValue;
    public T animValue;
}