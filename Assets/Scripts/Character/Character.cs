using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Transform visuals;

    public Transform Visuals { get => visuals; }
}

