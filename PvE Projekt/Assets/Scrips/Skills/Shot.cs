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
        bullet.transform.position = Vector3.zero; //only for debugging!
        LinearMovement lm = bullet.GetComponent<LinearMovement>();
        lm.StartMovement(spawnPoint.position, spawnPoint.position + spawnPoint.forward * maximumDistance);
        //bullet.GetComponent<LinearMovement>().Active = true;
        //bullet.GetComponent<LinearMovement>().Speed = 5f;
        //bullet.transform.position = transform.position;
        //bullet.GetComponent<LinearMovement>().Direction = transform.forward;
        base.Use();
    }

    protected override void InitializeVFX()
    {
        bullet = Instantiate(vfx, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<LinearMovement>();
    }
}
