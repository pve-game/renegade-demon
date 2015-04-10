using UnityEngine;
using System.Collections.Generic;

public class AttackAbility : Ability
{
    /// <summary>
    /// Damage that the shot does
    /// </summary>
    [SerializeField]
    protected int damage = 100;
    public int Damage { get { return damage; } }
    /// <summary>
    /// Animation speed
    /// </summary>
    [SerializeField]
    protected float speed = 1f;

    /// <summary>
    /// Determine which layer should be checked for collisions
    /// </summary>
    protected int collisionLayer = 0;
    [SerializeField]
    protected string collisionLayerName = "Default";

    /// <summary>
    /// Effects that should be applied to targets
    /// </summary>
    protected List<ImpactEffect> impactEffects = new List<ImpactEffect>();
    protected List<TargetSelection> targetSelectors = new List<TargetSelection>();

    protected override void Initialize()
    {
        base.Initialize();
        collisionLayer = LayerMask.NameToLayer(collisionLayerName);
    }

    /// <summary>
    /// Callback function for level up.
    /// Increases damage by 10% per level by default.
    /// </summary>
    /// <param name="level">reached level</param>
    protected override void LevelUpHandler(int level)
    {
        damage = damage + damage * level / 10;
    }

    public void AddSelector(TargetSelection selector)
    {
        targetSelectors.Add(selector);
    }

    public void RemoveSelector(TargetSelection selector)
    {
        targetSelectors.Remove(selector);
    }

    public void AddEffect(ImpactEffect effect)
    {
        impactEffects.Add(effect);
    }

    public void RemoveEffect(ImpactEffect effect)
    {
        impactEffects.Remove(effect);
    }
}

