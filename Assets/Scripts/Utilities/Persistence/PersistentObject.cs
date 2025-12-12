using UnityEngine;

public class PersistentObject : MonoBehaviour
{
    void Awake()
    {
        transform.SetParent(null);
        DontDestroyOnLoad(this);
    }
}
