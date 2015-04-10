using UnityEngine;
using System.Collections.Generic;

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

    ///// <summary>
    ///// Bullet pool
    ///// </summary>
    //[SerializeField]
    //private ObjectPool pool = null;

    private GameObject[] pool = null;


    public override void Use()
    {
        //bullet = pool.GetObject();
        //bullet.transform.position = Vector3.zero; //only for debugging!
        bullet = GetObject();
        if (bullet == null) return;
        bullet.SetActive(true);
        //bullet.GetComponent<DamageOnHit>().Damage = damage;
        
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
        //pool.Initialize(vfxNumber);
        
        //set up the collision properties
        impactEffects.Add(new DamageEffect(damage));
        targetSelectors.Add(new TargetSelection());
        InitializePool();

    }

    private void InitializePool()
    {
        pool = new GameObject[vfxNumber];
        for (int i = 0; i < vfxNumber; i++)
        {
            //shot gets a shot prefab that needs to be instantiated
            GameObject bullet = Instantiate(vfx, transform.position, transform.rotation) as GameObject;

            //rigidbody is needed to perform collision detection
            //as we might re-use the bullet prefab for other purposes
            //the rigidbody is added dynamically
            Rigidbody rb = bullet.AddComponent<Rigidbody>();
            //disable gravity as the shot should fly straight forward
            rb.useGravity = false;

            //on the prefab there should be a collider prepared
            //to fit the size of the model
            Collider c = bullet.GetComponent<Collider>();
            if (c != null)
            {
                c.enabled = true;
            }

            //straight forward movement
            bullet.AddComponent<LinearMovement>();

            //prepare collision handler
            TargetSelectionOnCollision collisionSelection = bullet.AddComponent<TargetSelectionOnCollision>();
            //add the set up target selection methods and impact effects
            //collisionSelection.AddSelectors(targetSelectors);
            //collisionSelection.AddEffects(impactEffects);
            collisionSelection.Selectors = targetSelectors;
            collisionSelection.Effects = impactEffects;

            collisionSelection.CollisionLayer = collisionLayer;
            //stop movement on hit
            collisionSelection.onHitOccured += bullet.GetComponent<LinearMovement>().StopMovement;

            ////apply damage on hit
            //DamageOnHit doh = bullet.AddComponent<DamageOnHit>();

            ////check only specified layer
            //doh.CollisionLayer = collisionLayer;
            //doh.Damage = damage;
            ////stop movement on hit
            //doh.onHitOccured += bullet.GetComponent<LinearMovement>().StopMovement;

            //if skill can level
            if (skillExperience != null)
            {
                //get experience on successful hit
                //doh.onHitOccured += GainExperience;
                collisionSelection.onHitOccured += GainExperience;
                skillExperience.onLevelChanged += LevelUpHandler;
            }

            //GameObject go = (GameObject)Instantiate(vfx, transform.position, transform.rotation);
            //go.AddComponent<LinearMovement>();
            bullet.SetActive(false);
            //pool.AddObject(bullet);
            pool[i] = bullet;
        }
    }

    protected override void LevelUpHandler(int level)
    {
        base.LevelUpHandler(level);
        //bullet.GetComponent<DamageOnHit>().Damage = damage;
    }

    private GameObject GetObject()
    {
        GameObject go = null;
        for (int i = 0; i < pool.Length; i++)
        {
            GameObject g = pool[i];
            if (!g.activeInHierarchy)
            {
                go = g;
                break;
            }
        }
        return go;
    }

    
}
