using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOTransformSetter : MonoBehaviour
{
    [SerializeField] private SOTransform transformVariable;
    [SerializeField] private Transform transformToSet;
    private void Awake()
    {
        transformVariable.value = transformToSet;
    }
}
