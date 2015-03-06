using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for arbitrary skills
/// </summary>
public abstract class Ability : MonoBehaviour
{
    /// <summary>
    /// Ability name for displaying
    /// </summary>
    [SerializeField]
    protected string name = "";
    public string Name { get { return name; } }
    /// <summary>
    /// Visual effect of the ability
    /// </summary>
    [SerializeField]
    protected GameObject vfx = null;
    /// <summary>
    /// Origin of the vfx
    /// </summary>
    [SerializeField]
    protected Transform spawnPoint = null;

    /// <summary>
    /// Upper range limit of the ability
    /// </summary>
    [SerializeField]
    protected float maximumDistance = 0f;

    /// <summary>
    /// Cooldown of the ability
    /// </summary>
    [SerializeField]
    protected float cooldown = 0f;
    public float Cooldown { get { return cooldown; } }

    /// <summary>
    /// Description of the spell
    /// </summary>
    [SerializeField]
    protected string description = "";
    public string Description { get { return description; } }

    private float timeSinceLastUse = 0f;
    public bool Ready { get { return Time.time - timeSinceLastUse > cooldown; } }
    
    public void Awake()
    {
        InitializeVFX();
    }

    public virtual void Use()
    {
        //save the time at the beginning of the frame
        timeSinceLastUse = Time.time;
    }
    protected abstract void InitializeVFX();

}
