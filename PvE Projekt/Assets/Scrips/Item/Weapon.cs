using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for weapons that contains a leveling feature
/// </summary>
/// <remarks>
/// Author: Martin Wettig
/// </remarks>
public abstract class Weapon : MonoBehaviour 
{
    /// <summary>
    /// Experience for a successful hit
    /// </summary>
    [SerializeField]
    protected int experienceForSuccessfulUsage = 1;

    /// <summary>
    /// Weapon damage
    /// </summary>
    [SerializeField]
    protected int damage = 1;
    public int Damage { get { return damage; } }

    /// <summary>
    /// Weapon experience
    /// </summary>
    protected Experience exp = null;

    public void Awake()
    {
        Initialize();
    }
	
    protected virtual void Initialize()
    {
        exp = new Experience();
        exp.onLevelChanged += LevelUpHandler;
    }

    protected void LevelUpHandler(int level)
    {
        damage = damage + damage * level / 10;
    }

    protected void GainExperience()
    {
        exp.AddExperience(experienceForSuccessfulUsage);
    }


}
