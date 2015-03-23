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
        //prepare appropriate collision layer first
        base.Initialize();
        //shot gets a shot prefab that needs to be instantiated
        bullet = Instantiate(vfx, transform.position, transform.rotation) as GameObject;

        //rigidbody is needed to perform collision detection
        //as we might re-use the bullet prefab for other purposes
        //the rigidbody is added dynamically
        Rigidbody rb = bullet.AddComponent<Rigidbody>();
        //disable gravity as the shot should fly straight forward
        rb.useGravity = false;

        //on the prefab there should be a collider prepared
        //to fit the size of the model
        Collider c = bullet.GetComponent<Collider>();
        if (c!= null)
        {
            c.enabled = true;
        }

        //straight forward movement
        bullet.AddComponent<LinearMovement>();

        //apply damage on hit
        DamageOnHit doh = bullet.AddComponent<DamageOnHit>();
       
        //check only specified layer
        doh.CollisionLayer = collisionLayer;
        doh.Damage = damage;
        //stop movement on hit
        doh.onHitOccured += bullet.GetComponent<LinearMovement>().StopMovement;
        
        //if skill can level
        if (skillExperience != null)
        {
            //get experience on successful hit
            doh.onHitOccured += GainExperience;
            skillExperience.onLevelChanged += LevelUpHandler;
        }
        //pool = new ObjectPool(vfxNumber);
        //for (int i = 0; i < vfxNumber; i++)
        //{

        //    GameObject go = Instantiate(vfx, transform.position, transform.rotation) as GameObject;
        //    go.AddComponent<LinearMovement>();
        //    pool.AddObject(go);
        //}
    }

    protected override void LevelUpHandler(int level)
    {
        base.LevelUpHandler(level);
        bullet.GetComponent<DamageOnHit>().Damage = damage;
        Debug.Log(damage);
        Debug.Log(skillExperience.CurrentLevel);
    }
}
