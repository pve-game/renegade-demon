using UnityEngine;
using System.Collections;

/// <summary>
/// This class represents the attack state of the archer enemy.
/// The archer attacks the target from the distance and walks around 
/// to make it more difficult for the Player to shot him.
/// </summary>
/// <remarks>
/// Author: Sebastian Borsch
/// </remarks>

[RequireComponent(typeof(NPCControl))]
[RequireComponent(typeof(VisualDetection))]
[RequireComponent(typeof(AudioDetection))]

public class A_Attack : FSMState {
    
    /// <summary>
    /// Implemets the audio detection to this state.
    /// </summary>
    private AudioDetection detectionHear;

    /// <summary>
    /// Implemets the visual detetcion to this state.
    /// </summary>
    private VisualDetection detectionSee;

    /// <summary>
    /// Implements the NPC Control to this state.
    /// </summary>
    private NPCControl objects;

    /// <summary>
    /// Necessary to use the Navigation Mesh for the NPC movement.
    /// </summary>
    NavMeshAgent agent;

	// Use this for initialization
	void Awake () 
    {
        stateID = StateID.A_Attack;

        // Get the components of the audio and visual detection an the
        // NPC Control to use this in the state.
        detectionHear = GetComponent<AudioDetection>();
        detectionSee = GetComponent<VisualDetection>();
        objects = GetComponent<NPCControl>();

        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Reason(GameObject player, GameObject npc)
    {
        if(!detectionSee.detected && detectionHear.detected)
        {
            agent.Stop();
            npc.GetComponent<NPCControl>().SetTransition(Transition.A_HeardSomething);
        }

        if (!detectionHear.detected && !detectionSee.detected)
        {
            npc.GetComponent<NPCControl>().SetTransition(Transition.A_LostPlayer);
        }
    }

    public override void Act(GameObject player, GameObject npc)
    {
        Debug.Log("Attack from Distance!");
    }

    
}
