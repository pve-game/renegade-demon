using UnityEngine;
using System.Collections;

/// <summary>
/// Reduces the taken damage
/// </summary>
/// <remarks>
/// Author:Martin Wettig
/// </remarks>
public class ReducedDamage : Talent
{
    [SerializeField]
    private Health health = null;

    /// <summary>
    /// Denotes the amount of damage reduction.
    /// Default: 1f, i.e. no damage reduction at all, 0.5f means half of original damage value.
    /// </summary>
    [SerializeField]
    private float modifier = 1f;
  
    public override void Learn()
    {
        health.DamageModifier -= modifier;
    }

    public override void UnLearn()
    {
        health.DamageModifier += modifier;
    }
}
