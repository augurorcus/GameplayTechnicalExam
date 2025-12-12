using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOCameraSetter : MonoBehaviour
{
    [SerializeField] private SOCamera cameraToSet;
    [SerializeField] private Camera cameraReference;

    private void Awake()
    {
        if (cameraReference is null)
        {
            cameraReference = GetComponent<Camera>();
        }

        cameraToSet.value = cameraReference;
    }
}
