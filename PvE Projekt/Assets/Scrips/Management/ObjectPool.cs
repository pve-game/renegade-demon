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
public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    private GameObject[] pool;

    private List<GameObject> pool2;//old

    /// <summary>
    /// ObjectPool size
    /// </summary>
    [SerializeField]
    private int capacity = 1;
    
    private int current = 0;//old

    public UnityEvent evt;

    public void Awake()
    {
        pool = new GameObject[capacity];
        for (int i = 0; i < capacity; i++)
        {
            GameObject go = (GameObject)Instantiate(prefab);
            go.SetActive(false);
            pool[i] = go;
        }
    }

    /// <summary>
    /// Creates the pool
    /// </summary>
    /// <param name="capacity">number of elements in the pool</param>
    public void Initialize(int capacity)
    {
        this.capacity = capacity;
        pool = new GameObject[capacity];

        pool2 = new List<GameObject>(capacity);


        string s = (pool == null) ? "yes" : "no";

        Debug.Log("init: pool-null? " + s);
    }

    /// <summary>
    /// Gets the next available object
    /// </summary>
    /// <returns>a usable object</returns>
    public GameObject GetObject()
    {
        string s = (pool == null) ? "yes" : "no";
        Debug.Log("get-object: pool-null? " + s);

        GameObject go = null;
        for (int i = 0; i < pool.Length; i++)
        {
            if (!pool[i].activeSelf)
            {
                go = pool[i];
                Debug.Log(go);
                break;
            }
        }
        //Debug.Log("Pool2-count: " + pool2.Count);
        //for (int i = 0; i < pool2.Count; i++)
        //{
        //    if (!pool2[i].activeSelf)
        //    {
        //        go = pool[i];
        //        Debug.Log(go);
        //        break;
        //    }
        //}

        return go;
    }

  
    public void AddObject(GameObject go)
    {
        string s = (pool == null) ? "yes" : "no";

        Debug.Log("add-object: pool-null? " + s);
        if (pool.Length != capacity)
        {
            pool[current] = go;
            //pool2.Add(go);
            if (pool[current] == null)
                Debug.Log("is null");
            ++current;
        }

        int nulls = 0;
        for (int i = 0; i < pool.Length; i++)
            if (pool[i] == null) ++nulls;

        Debug.Log("nulls in pool: " + nulls);
    }
}
