using UnityEngine;
using System.Collections;

/// <summary>
/// Removes an object from the world if its health reaches zero
/// </summary>
/// <remarks>
/// Author:Martin Wettig
/// </remarks>
public class DestroyOnDeath : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        Health health = GetComponent<Health>();
        health.onHealthChanged += DestroySelf;
    }

    public void DestroySelf(float p)
    {
        Destroy(gameObject);
    }
}
