using UnityEngine;
using System.Collections;

/// <summary>
/// This class represents the idle state of the archer NPC. In this state,
/// the enemy does nothing
/// </summary>
/// <remarks>
/// Author: Sebastian Borsch
/// </remarks>

[RequireComponent(typeof(NPCControl))]
[RequireComponent(typeof(VisualDetection))]
[RequireComponent(typeof(AudioDetection))]

public class A_Idle : FSMState {

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
    /// This shows the point, wich the archer enemy should guard.
    /// </summary>
    [SerializeField]
    private Transform guardingPoint;

    /// <summary>
    /// Shows if the archer is at his position or not.
    /// </summary>
    private bool atGuardingPosition;

    /// <summary>
    /// Necessary to use the Navigation Mesh for the NPC movement.
    /// </summary>
    NavMeshAgent agent;

	// Use this for initialization
	void Awake ()
    {
        // Declares the state
        stateID = StateID.A_Idle;    
	    
        // Get the components of the audio and visual detection an the
        // NPC Control to use this in the state.
        detectionHear = GetComponent<AudioDetection>();
        detectionSee = GetComponent<VisualDetection>();
        objects = GetComponent<NPCControl>();

        agent = GetComponent<NavMeshAgent>();
    }

    public override void Reason(GameObject player, GameObject npc)
    {
        throw new System.NotImplementedException();
    }

    public override void Act(GameObject player, GameObject npc)
    {
        /// <summary>
        /// Calculate the distance between the archer and his guarding position
        /// </summary>
        float DistcanceToGuardingPosition = (guardingPoint.position - transform.position).sqrMagnitude;

        if (DistcanceToGuardingPosition > 3.0f)
        {
            GetBackToGuardingPosition(DistcanceToGuardingPosition);
        }
        else
        {
            Debug.Log("Play idle animation here");
        }
    }

    /// <summary>
    /// Let the archer find his way back to his guarding Position
    /// </summary>
    private void GetBackToGuardingPosition(float distance)
    {
        agent.SetDestination(guardingPoint.position);

        if (distance < 5.0f)
        {
            agent.speed = Mathf.Lerp(agent.speed, 0.1f, 2.0f);
        }
    }
}
