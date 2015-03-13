using UnityEngine;
using System.Collections;

public class AoE : AttackAbility
{
    private GameObject bullet = null;

    public override void Use()
    {
        OrbitingMovement om = bullet.GetComponent<OrbitingMovement>();
        om.StartMovement(transform);
        base.Use();
    }

    protected override void Initialize()
    {
        bullet = Instantiate(vfx, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<OrbitingMovement>();
    }
}
