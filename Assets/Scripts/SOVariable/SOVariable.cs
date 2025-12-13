using UnityEngine;

public abstract class SOVariable<T> : ScriptableObject
{
    public T value;

    public virtual T ResetValue() { return default(T); }
}
