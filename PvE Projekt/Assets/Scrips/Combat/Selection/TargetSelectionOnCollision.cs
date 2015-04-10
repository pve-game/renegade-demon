using UnityEngine;
using System.Collections.Generic;

public class TargetSelectionOnCollision : MonoBehaviour
{
    private List<TargetSelection> selectors = new List<TargetSelection>();
    public List<TargetSelection> Selectors { set { selectors = value; } }
    private List<ImpactEffect> effects = new List<ImpactEffect>();
    public List<ImpactEffect> Effects { set { effects = value; } }

    /// <summary>
    /// Determine which layer should be checked for collisions
    /// </summary>
    private int collisionLayer = 0;
    //public string collisionLayerName = "Default";
    public int CollisionLayer { get { return collisionLayer; } set { collisionLayer = value; } }
    /// <summary>
    /// Notify registered classes if a hit occured
    /// </summary>
    public delegate void HitOccured();
    public HitOccured onHitOccured;


    public void OnTriggerEnter(Collider col)
    {
        //if other object is not on the desired layer skip
        //if (col.gameObject.layer != collisionLayer) return;
        //for all selection methods
        for(int i = 0; i < selectors.Count; i++)
        {
            //find the corresponding targets
            List<GameObject> targets = selectors[i].DetermineTargets(col.gameObject, collisionLayer);
            //apply the attached impact effects
            for (int j = 0; j < targets.Count; j++)
            {
                if (targets[j] == gameObject) continue; //skip itself
                targets[j].GetComponent<ImpactEffectProcessorManager>().SendEffects(effects);
            }
        }
    }

    public void AddSelectors(List<TargetSelection> selectors)
    {
        for(int i = 0; i < selectors.Count; i++)
            AddSelector(selectors[i]);
    }
    public void AddSelector(TargetSelection selector)
    {
        selectors.Add(selector);
    }

    public void RemoveSelector(TargetSelection selector)
    {
        selectors.Remove(selector);
    }

    public void AddEffects(List<ImpactEffect> effects)
    {
        for (int i = 0; i < selectors.Count; i++)
            AddEffect(effects[i]);
    }

    public void AddEffect(ImpactEffect effect)
    {
        effects.Add(effect);
    }

    public void RemoveEffect(ImpactEffect effect)
    {
        effects.Remove(effect);
    }
}
