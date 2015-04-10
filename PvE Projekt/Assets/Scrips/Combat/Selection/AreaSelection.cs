using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// Allows to select objects around a centered object
/// </summary>
/// <remarks>
/// Author: Martin Wettig
/// </remarks>
public class AreaSelection : TargetSelection
{
    private float radius = 1f;
  //  private int layer = 0;

    public AreaSelection(float checkRadius)//, int checkLayer)
    {
        radius = checkRadius;
       // layer = checkLayer;
    }

    /// <summary>
    /// Selects objects in the neighbourhood of the given object
    /// </summary>
    /// <param name="impactObject">center object</param>
    /// <returns>list of objects in the neighbourhood</returns>
    public override List<GameObject> DetermineTargets(GameObject impactObject, int layer)
    {
        List<GameObject> targets = new List<GameObject>();//base.DetermineTargets(impactObject, layer);
        //targets.RemoveAt(0); //remove initial object from area selection
        Collider[] hits = Physics.OverlapSphere(impactObject.transform.position, radius, 1 << layer);
        for (int i = 0; i < hits.Length; i++)
        {
            targets.Add(hits[i].gameObject);
        }
        return targets;
    }
}
