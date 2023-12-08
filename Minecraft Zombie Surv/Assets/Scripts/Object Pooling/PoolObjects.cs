using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjects<T> where T : PoolableObject
{
    private T prefab;
    private bool expanding;
    private Transform parent;
    private List<T> objectsInPool;

    public PoolObjects(T prefab, int startCount, bool expanding, Transform parent = null)
    {
        this.prefab = prefab;
        this.expanding = expanding;
        this.parent = parent;
        objectsInPool = new List<T>();

        for (int i = 0; i < startCount; i++)
        {
            CreateNewObject();
        }
    }

    public T CreateNewObject()
    {
        T newObj = Object.Instantiate(prefab, parent);
        newObj.gameObject.SetActive(false);
        objectsInPool.Add(newObj);
        return newObj;
    }

    public T GetObject()
    {
        foreach (T obj in objectsInPool)
        {
            if (!obj.gameObject.activeSelf)
            {
                obj.gameObject.SetActive(true);
                return obj;
            }
        }

        if (expanding)
        {
            T newObj = CreateNewObject();
            newObj.gameObject.SetActive(true);
            return newObj;
        }

        return null;
    }
}
