using UnityEngine;
using System.Collections;

/// <summary>
/// Simple shot ability
/// </summary>
public class Shot : AttackAbility 
{
    /// <summary>
    /// holds the bullet instance
    /// </summary>
    private GameObject bullet = null;
    [SerializeField]
    private int vfxNumber = 10;
    

    ObjectPool pool = null;

   
    public override void Use()
    {
        //bullet = pool.GetObject();
        bullet.transform.position = Vector3.zero; //only for debugging!
        LinearMovement lm = bullet.GetComponent<LinearMovement>();
        lm.StartMovement(spawnPoint.position, spawnPoint.position + spawnPoint.forward * maximumDistance);
        
        //--old
        //bullet.GetComponent<LinearMovement>().Active = true;
        //bullet.GetComponent<LinearMovement>().Speed = 5f;
        //bullet.transform.position = transform.position;
        //bullet.GetComponent<LinearMovement>().Direction = transform.forward;
        base.Use();
    }

    protected override void Initialize()
    {
        bullet = Instantiate(vfx, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<LinearMovement>();
        bullet.AddComponent<DamageOnHit>();
        DamageOnHit doh = bullet.GetComponent<DamageOnHit>();
        doh.Damage = damage;
        doh.onHitOccured += bullet.GetComponent<LinearMovement>().StopMovement;
        //pool = new ObjectPool(vfxNumber);
        //for (int i = 0; i < vfxNumber; i++)
        //{

        //    GameObject go = Instantiate(vfx, transform.position, transform.rotation) as GameObject;
        //    go.AddComponent<LinearMovement>();
        //    pool.AddObject(go);
        //}
    }
}
