﻿using UnityEngine;
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
        base.Initialize();
        bullet = Instantiate(vfx, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<OrbitingMovement>();
        OrbitingMovement om = bullet.GetComponent<OrbitingMovement>();
        om.Speed = speed;
        om.Distance = radius;
        om.Duration = Duration;
        if(skillExperience != null)
            skillExperience.onLevelChanged += LevelUpHandler;
    }

    private void ApplyDamage()
    {
        //for all objects within the radius
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, 1 << collisionLayer);
        foreach (Collider col in hits)
        {
            Debug.Log(col.name);
            //get their health component if present
            Health h = col.gameObject.GetComponent<Health>();
            if (h != null)
            {
                //apply damage and increase collected experience
                h.addHealth(-damage);
                GainExperience();
            }
        }

    }

    protected override void LevelUpHandler(int level)
    {
        base.LevelUpHandler(level);
        Debug.Log(damage);
        Debug.Log(skillExperience.CurrentLevel);
    }
}
