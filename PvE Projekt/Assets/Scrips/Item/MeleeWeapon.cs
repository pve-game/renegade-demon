using UnityEngine;
using System.Collections;

/// <summary>
/// Melee weapon that applies damage directly
/// </summary>
/// <remarks>
/// Author: Martin Wettig
/// </remarks>
[RequireComponent(typeof(DamageOnHit))]
public class MeleeWeapon : Weapon
{
    protected override void Initialize()
    {
        base.Initialize();
        DamageOnHit doh = GetComponent<DamageOnHit>();
        doh.onHitOccured += GainExperience;
    }


}
