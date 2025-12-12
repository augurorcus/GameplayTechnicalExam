using UnityEngine;
using Sirenix.OdinInspector;

public abstract class ComponentPattern<T> : MonoBehaviour
{
    [BoxGroup("Entity Reference"), SerializeField] protected T entity;

    protected virtual void Awake()
    {
        if (entity is null)
            entity = GetComponent<T>();
    }

    protected virtual void OnValidate()
    {
        if (entity is null)
            entity = GetComponent<T>();
    }
}
