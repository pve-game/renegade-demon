using UnityEngine;
using System.Collections;

public class AttackAbility : Ability
{
    /// <summary>
    /// Damage that the shot does
    /// </summary>
    [SerializeField]
    protected int damage = 100;
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
}
