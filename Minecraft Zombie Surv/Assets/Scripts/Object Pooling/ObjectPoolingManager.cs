using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    [SerializeField] private PoolableObject[] poolablePrefabs;

    private Dictionary<string, PoolObjects<PoolableObject>> cachedPoolables;

    private void OnEnable()
    {
        ServiceLocator.AddService(this);

        cachedPoolables = new Dictionary<string, PoolObjects<PoolableObject>>();

        foreach (PoolableObject poolable in poolablePrefabs)
        {
            PoolObjects<PoolableObject> newPool = new PoolObjects<PoolableObject>(poolable, 2, true, transform);
            cachedPoolables.Add(poolable.PoolID, newPool);
        }
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    public T GetPoolable<T>(T prefab, int poolableCount = 2) where T : PoolableObject
    {
        if (cachedPoolables.ContainsKey(prefab.PoolID))
        {
            return cachedPoolables[prefab.PoolID].GetObject() as T;
        }
        else
        {
            PoolObjects<PoolableObject> newPool = new PoolObjects<PoolableObject>(prefab, poolableCount, true);
            cachedPoolables.Add(prefab.PoolID, newPool);
            return cachedPoolables[prefab.PoolID].GetObject() as T;
        }
    }
}