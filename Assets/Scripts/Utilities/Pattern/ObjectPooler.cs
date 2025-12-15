using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#pragma warning disable 0649

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private string poolerName;
    [SerializeField] GameObject pooledObject;
    [SerializeField] public bool prewarm;
    [SerializeField, ShowIf(nameof(prewarm))] public int prewarmAmount;

    private Queue<GameObject> objectPool;

    private void Awake()
    {
        objectPool = new Queue<GameObject>();
        if (prewarm) PrewarmObjectPool();
    }

    public void PrewarmObjectPool()
    {
        for (int i = 0; i < prewarmAmount; i++)
        {
            GameObject newWarmObject = CreateNewObject(pooledObject);
            ReturnGameObject(newWarmObject);
        }
    }

    public GameObject GetObject()
    {
        if (objectPool.Count == 0) return CreateNewObject(pooledObject);

        GameObject retrievedObject = objectPool.FirstOrDefault(x => x.gameObject.activeSelf);

        if (retrievedObject != null)
        {
            retrievedObject.gameObject.SetActive(true);
            return retrievedObject;
        }
        else return CreateNewObject(pooledObject);

    }

    private GameObject CreateNewObject(GameObject objectPrefabToCreate)
    {
        GameObject newGO = Instantiate(objectPrefabToCreate);
        newGO.name = objectPrefabToCreate.name;
        return newGO;
    }

    public void ReturnGameObject(GameObject gameObject)
    {
        objectPool.Enqueue(gameObject);
        gameObject.SetActive(false);
    }
}
