using UnityEngine;
using System.Collections;

/// <summary>
/// Simple shot ability
/// </summary>
public class Shot : Ability 
{
    /// <summary>
    /// holds the bullet instance
    /// </summary>
    private GameObject bullet = null;

    public override void Use()
    {
        bullet.transform.position = Vector3.zero;
        bullet.GetComponent<LinearMovement>().Active = true;
        bullet.GetComponent<LinearMovement>().Speed = 5f;

        base.Use();
    }

    protected override void InitializeVFX()
    {
        bullet = Instantiate(vfx, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<LinearMovement>();

    }
}
