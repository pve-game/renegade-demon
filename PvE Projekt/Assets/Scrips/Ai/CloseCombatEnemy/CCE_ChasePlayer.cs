
﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

/// <summary>
/// This state represents the chasing state of the NPC.
/// In this state the NPC tries to reach the Players position.
/// If he looses the Player he will look out for him in the searching state.
/// If he get close to the Players position he will change to the attack state and
/// attack the Player
/// </summary>
/// 
/// <remarks>
/// Author: Sebastian Borsch
/// </remarks>
[RequireComponent(typeof(NPCControl))]
[RequireComponent(typeof(AudioDetection))]
[RequireComponent(typeof(VisualDetection))]

public class CCE_ChasePlayer : FSMState {

    /// <summary>
    /// Implemets the audio detection to this state.
    /// </summary>
    private VisualDetection detectionSee;

    /// <summary>
    /// Implemets the visual detetcion to this state.
    /// </summary>
    private AudioDetection detectionHear;

    /// <summary>
    /// Implements the NPC Control to this state.
    /// </summary>
    private NPCControl objects;

    /// <summary>
    /// Shows the spped with wich the NPC will chase the Player
    /// </summary>
    [SerializeField]
    private float chasingSpeed = 0.0f;

    /// <summary>
    /// Shwos the distance at wich the NPC will attack the Player
    /// </summary>
    [SerializeField]
    private float AttackDistance;

    /// <summary>
    /// Necessary to use the Navigation Mesh for the NPC movement.
    /// </summary>
    NavMeshAgent agent;


	void Awake () {
        // Declares the state
        stateID = StateID.ChasingPlayer;

        // Get the components of the NPC Control and the audio and visual detection
        detectionHear = GetComponent<AudioDetection>();
        detectionSee = GetComponent<VisualDetection>();
        objects = GetComponent<NPCControl>();

        agent = GetComponent<NavMeshAgent>();
	}


    public override void Reason(UnityEngine.GameObject player, UnityEngine.GameObject npc)
    {
        // If the Player is out of visual range, the NPC will check if he's still in earshot
        // or if he lost him completly
        if (!detectionSee.detected && detectionHear.detected)
        {
            npc.GetComponent<NPCControl>().SetTransition(Transition.HeardSomething);
        }

        if (!detectionSee.detected && !detectionHear.detected)
        {
            npc.GetComponent<NPCControl>().SetTransition(Transition.LostPlayer);
        }

        // If the Player is in range, the NPC will attack the Player
        if(objects.DistanceToPlayer <= AttackDistance)
        {
            npc.GetComponent<NPCControl>().SetTransition(Transition.Attack);
        }
    }

    public override void Act(UnityEngine.GameObject player, UnityEngine.GameObject npc)
    {
        // the NPC will increase his speed to chase the Player and 
        // sets his target position to the current Player position.
        Debug.Log(" Ich kriege dich!  ");

        agent.speed = chasingSpeed;

        agent.SetDestination(objects.PlayerPosition.position);
    }


}
