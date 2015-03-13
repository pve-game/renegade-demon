using UnityEngine;
using System.Collections;

/// <summary>
/// Facilitates the re-use of already instantiated objects
/// </summary>
/// <remarks>
/// Author: Martin Wettig
/// </remarks>
public class ObjectPool
{
    private GameObject[] pool;
    private int capacity = 1;
    private int current = 0;
    public ObjectPool(int capacity)
    {
        pool = new GameObject[capacity];
        this.capacity = capacity;
    }

    /// <summary>
    /// gets the next available object
    /// </summary>
    /// <returns>a usable object</returns>
    public GameObject GetObject()
    {
        GameObject go = null;
        for (int i = 0; i < pool.Length; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                go = pool[i];
                Debug.Log(go);
                break;
            }
        }
        return go;
    }

    public void AddObject(GameObject go)
    {
        if(pool.Length != capacity)
        {
            pool[current] = go;
            ++current;
        }
    }
}
