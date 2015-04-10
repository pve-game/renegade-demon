using UnityEngine;
using System.Collections;

/// <summary>
/// Encapsulates damage values.
/// It further supports values required for damage over time.
/// </summary>
/// <remarks>
/// Author: Martin Wettig
/// </remarks>
public class DamageEffect : ImpactEffect
{
    private int damage = 0;
    public int Damage { get { return damage; } }
    
    private float duration;
    public float Duration { get { return duration; } }
    
    private int numberOfTicks;
    public int NumberOfTicks { get { return numberOfTicks; } }

    public DamageEffect(int damageValue, float timespan = 0f, int tickCount = 1)
    {
        damage = damageValue;
        duration = timespan;
        numberOfTicks = tickCount;
    }

}
