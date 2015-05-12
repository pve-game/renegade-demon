using UnityEngine;
using System.Collections;

/// <summary>
/// This state is part of the close combat enemy and represents the status
/// when the NPC had herad some noise and now looks for it source. 
/// The NPC will walk to the position where he detected the noise and check if he can here other noises nearby.
/// If he hears some, he will keep searching for them, otherwise he will take a few looks around, and walk back 
/// the position he should guard.
/// </summary>
/// 
/// <remarks>
/// Author: Sebastian Borsch
/// </remarks>
/// 
[RequireComponent(typeof(NPCControl))]
[RequireComponent(typeof(AudioDetection))]
[RequireComponent(typeof(VisualDetection))]

public class CCE_Search : FSMState
{

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

    private Vector3 playerPosition;

    /// <summary>
    /// Shows if the NPC is still searching for the Player.
    /// </summary>
    private bool searching;

    /// <summary>
    /// Shows the distance between the current position of the NPC and
    /// the audio source wich takes its atention.
    /// </summary>
    private float DistanceToAudioSource;

    /// <summary>
    /// The Timer for that State. Important to get get the time the NPC should look around.
    /// </summary>
    private float Timer = 0.0f;

    /// <summary>
    /// Shows how long the NPC should look aroud after finding nothing.
    /// </summary>
    private float TimeToLookAround = 2.0f;

    /// <summary>
    /// Necessary to use the Navigation Mesh for the NPC movement.
    /// </summary>
    NavMeshAgent agent;

    // Use this for initialization
    void Awake()
    {

        // Deklares the state 
        stateID = StateID.Searching;

        // Get the components of the audio and visual detetction.
        detectionHear = GetComponent<AudioDetection>();
        detectionSee = GetComponent<VisualDetection>();

        // Get the components of the NPC Control.
        objects = GetComponent<NPCControl>();

        // Get the component of the Nav Mesh
        agent = GetComponent<NavMeshAgent>();

    }


    public override void Reason(GameObject player, GameObject npc)
    {
        // If the NPC can not hear or see anything, he returns to his patrol an continues patroling.
        if (!searching && !detectionHear.detected)
        {
            npc.GetComponent<NPCControl>().SetTransition(Transition.LostPlayer);
        }


        // If the NPC sees the Player he will chase the Player
        if (detectionSee.detected && objects.SqrDistanceToPlayer > 25)

            if (detectionSee.detected && objects.SqrDistanceToPlayer > 25)
            {
                npc.GetComponent<NPCControl>().SetTransition(Transition.SawPlayer);
            }
    }

    public override void Act(GameObject player, GameObject npc)
    {

        TimeCounter();

        // Calculate the Distance between the audio source and the current NPC postition
        DistanceToAudioSource = Vector3.SqrMagnitude(playerPosition - transform.position);

        if (detectionHear.detected)
        {
            playerPosition = objects.PlayerPosition.position;
            agent.SetDestination(playerPosition);
            searching = true;
        }

        if (DistanceToAudioSource <= 2)
        {
            //WaitAndLookAround();
            searching = false;
        }
    }

    private void TimeCounter()
    {
        Timer += Time.deltaTime * 1;
    }

    private void WaitAndLookAround()
    {
        float LookAroundView = transform.rotation.y;
        Timer = 0.0f;
        agent.speed = Mathf.Lerp(agent.speed, 0.1f, 1.0f);


        if (Timer >= TimeToLookAround)
        {
            agent.speed = 1.0f;
            Timer = 0.0f;

        }
    }
}
