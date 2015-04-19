using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// WIP : DO NOT USE ATM
/// Facilitates the re-use of already instantiated objects
/// </summary>
/// <remarks>
/// Author: Martin Wettig
/// </remarks>
public class ObjectPool
{
    private GameObject[] pool;

    /// <summary>
    /// ObjectPool size
    /// </summary>
    private int capacity = 1;
    /// <summary>
    /// Keeps track of available space
    /// </summary>
    private int nextFreeSlot = 0;
    
    public ObjectPool(int size)
    {
        capacity = size;
        pool = new GameObject[size];
    }

    public void AddObject(GameObject obj)
    {
        //add the object only if there is still space
        if (nextFreeSlot < capacity)
        {
            pool[nextFreeSlot++] = obj;
        }
    }

    /// <summary>
    /// Gets the next available object
    /// </summary>
    /// <returns>a usable object</returns>
    public GameObject GetObject()
    {
        GameObject go = null;
        for (int i = 0; i < pool.Length; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }
  
        return go;
    }
}
