using UnityEngine;
using System.Collections;

/// <summary>
/// This class represents the state, when the NPC has heard something 
/// and now is more attented.
/// </summary>
/// <remarks>
/// Author: Sebastian Borsch
/// </remarks>

[RequireComponent(typeof(NPCControl))]
[RequireComponent(typeof(VisualDetection))]
[RequireComponent(typeof(AudioDetection))]

public class A_Attentive : FSMState {

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

    private Vector3 NPCPlayerVector;

    private Vector3 ViewDirection;

	// Use this for initialization
    void Awake()
    {
        // Declares the state
        stateID = StateID.A_Attentive;

        // Get the components of the audio and visual detection an the
        // NPC Control to use this in the state.
        detectionHear = GetComponent<AudioDetection>();
        detectionSee = GetComponent<VisualDetection>();
        objects = GetComponent<NPCControl>();

        agent = GetComponent<NavMeshAgent>();
    }

    public override void Reason(GameObject player, GameObject npc)
    {
        if (!detectionHear.detected && !detectionSee.detected)
        {
            npc.GetComponent<NPCControl>().SetTransition(Transition.A_LostPlayer);
        }

        if (detectionSee.detected)
        {
            npc.GetComponent<NPCControl>().SetTransition(Transition.A_SawPlayer);
        }
    }

    public override void Act(GameObject player, GameObject npc)
    {
       transform.LookAt(objects.PlayerPosition.position);

        // Later here should be the caluclation for a slow rotation of the NPC,
        // so that he looks directly to the position where he heard th noise.
        /*
        NPCPlayerVector = transform.position - objects.PlayerPosition.position;
        ViewDirection = Vector3.RotateTowards(transform.forward, NPCPlayerVector, agent.speed, 0.0f);
        */
    }
}
