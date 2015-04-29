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
        if (objects.DistanceToPlayer >= 32.5f)
        {
            randomStepDecision();
        }
    }

    /// <summary>
    /// The Archer will shot an arrow to the enemy
    /// </summary>
    private void shoot()
    {
        Debug.Log("Shoot  //  Attack from Distance!");
    }
   
    /// <summary>
    /// Get an random value wich decides in wich direction the archer will make his next step
    /// an execute the order given.
    /// </summary>
    private void randomStepDecision()
    {
        Vector3 newPosition;

        Vector3 left = new Vector3(0.0f, 0.0f, -40.0f);
        Vector3 right = new Vector3(0.0f, 0.0f, 40.0f);

        if (Random.value <= 0.5f)
        {
            newPosition = transform.position + left;
        }
        else
        {
           newPosition = transform.position + right;
        }

        agent.SetDestination(newPosition);
        if (transform.position == newPosition)
        {
            /// Hier an dieser stelle eigene rotation berechnen
            transform.LookAt(objects.PlayerPosition.position);
            shoot();
        }
    }


    
}
