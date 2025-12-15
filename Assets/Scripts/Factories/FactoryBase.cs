using UnityEngine;

public abstract class FactoryBase<T> : ScriptableObject
{
    public abstract T CreateProduct();
}
