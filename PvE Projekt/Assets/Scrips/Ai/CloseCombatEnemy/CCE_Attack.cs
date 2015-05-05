using UnityEngine;
using System.Collections;

/// <summary>
/// This state represents the attack state of the NPC. 
/// The mission is clear: Hit the Player. Hard. 
/// If the Player is too far away for close combat, the NPC will chase him.
/// </summary>
/// 
/// <remarks>
/// Author: Sebastian Borsch
/// </remarks>
[RequireComponent(typeof(NPCControl))]
[RequireComponent(typeof(VisualDetection))]
[RequireComponent(typeof(AudioDetection))]

public class CCE_Attack : FSMState
{

    /// <summary>
    /// Implements the NPC Control to this state.
    /// </summary>
    private NPCControl objects;

    /// <summary>
    /// Float that shows the distance between the Player and the NPC
    /// </summary>
    private float Distance;

    /// <summary>
    /// Variable that shows the distance at wich the Player is too far
    /// away from to NPC to get hit.
    /// </summary>
    [SerializeField]
    private float TransitionDistance;


    // Use this for initialization
    void Awake()
    {

        // Declares the current state
        stateID = StateID.AttackPlayer;

        // Get the component of the NPC Control
        objects = GetComponent<NPCControl>();

    }


    public override void Reason(GameObject player, GameObject npc)
    {

        // If the Player is too far away from the Player, he will change to the chasing state.
        if (objects.DistanceToPlayer > TransitionDistance)

            if (objects.DistanceToPlayer > TransitionDistance)
            {
                npc.GetComponent<NPCControl>().SetTransition(Transition.SawPlayer);
            }
    }

    public override void Act(GameObject player, GameObject npc)
    {
        // This Debug Log is placeholder for the call of the attack animation, 
        // wich will also calculate the damage the Player will get from the NPC.
        Debug.Log("Attack!");
    }
}