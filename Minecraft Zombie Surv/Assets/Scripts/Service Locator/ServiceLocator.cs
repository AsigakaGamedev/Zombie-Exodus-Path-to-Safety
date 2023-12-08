using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
    private static List<object> servises = new List<object>();

    public static void AddService(object newService)
    {
        if (!servises.Contains(newService))
        {
            servises.Add(newService);
        }
    }

    public static void RemoveService(object service)
    {
        if (servises.Contains(service))
        {
            servises.Remove(service);
        }
    }

    public static T GetService<T>()
    {
        foreach (object service in servises)
        {
            if (service is T) return (T)service;
        }

        throw new System.Exception($"Service {typeof(T)} not found!");
    }
}
