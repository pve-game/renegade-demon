using UnityEngine;
using System.Collections;

public abstract class AttackAbility : Ability
{
    /// <summary>
    /// Damage that the shot does
    /// </summary>
    [SerializeField]
    protected int damage = 100;

}
