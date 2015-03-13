using UnityEngine;
using System.Collections;

/// <summary>
/// Applies damage to the hit object
/// </summary>
/// <remarks>
/// Author: Martin Wettig
/// </remarks>
public class DamageOnHit : MonoBehaviour 
{
    [SerializeField]
    private int damage = 0;
    public int Damage { get { return damage; } set { damage = Mathf.Max(value, 0); } }

    /// <summary>
    /// Determine which layer should be checked for collisions
    /// </summary>
    private int collisionLayer = 0;
    public string collisionLayerName = "Default";

    /// <summary>
    /// Notify registered classes if a hit occured
    /// </summary>
    public delegate void HitOccured();
    public HitOccured onHitOccured;
    public HitOccured OnHitOccured { get { return onHitOccured; } }

    public void Awake()
    {
        collisionLayer = LayerMask.NameToLayer(collisionLayerName);
    }

    public void OnTriggerEnter(Collider col)
    {
        GameObject other = col.gameObject;
        if (other.layer == collisionLayer)
        {
            Health h = other.GetComponent<Health>();
            h.addHealth(-damage);
            if (onHitOccured != null)
                onHitOccured();
        }
    }
}
