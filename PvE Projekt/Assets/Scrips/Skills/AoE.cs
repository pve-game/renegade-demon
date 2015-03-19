using UnityEngine;
using System.Collections;

/// <summary>
/// Area of effect spell
/// </summary>
public class AoE : AttackAbility
{
    [SerializeField]
    private float radius = 3f;
    public float Radius { get { return radius; } set { radius = Mathf.Max(value, 0f); } }

    private GameObject bullet = null;

    public override void Use()
    {
        if (Ready)
        {
            OrbitingMovement om = bullet.GetComponent<OrbitingMovement>();
            om.StartMovement(transform);
            ApplyDamage();
            base.Use();
        }
    }

    protected override void Initialize()
    {
        bullet = Instantiate(vfx, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<OrbitingMovement>();
        OrbitingMovement om = bullet.GetComponent<OrbitingMovement>();
        om.Speed = speed;
        om.Distance = radius;
        om.Duration = Duration;
    }

    private void ApplyDamage()
    {
        //TODO: fix double intersection bug -> damage is applied twice
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, 1 << collisionLayer);
        Debug.Log("hits: " + hits.Length);
        foreach (Collider col in hits)
        {
            Health h = col.gameObject.GetComponent<Health>();
            if (h != null)
                h.addHealth(-damage);
        }

    }
}
