using UnityEngine;
using System.Collections;

/// <summary>
/// This state represents the idle state of the close combat NPC.
/// In this state the NPC will walk between to Waypoints to guard an area. 
/// At each Waypoint he will make a sort break and walk to the next Waypoint after if.
/// If the NPC detects some noise or see something he will change his state.
/// </summary>
/// 
/// <remarks>
/// Author: Sebastian Borsch
/// </remarks>
[RequireComponent(typeof(NPCControl))]
[RequireComponent(typeof(VisualDetection))]
[RequireComponent(typeof(AudioDetection))]

public class CCE_Patrol : FSMState
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

    /// <summary>
    /// The first Waypoint the NPC will use for his patroling.
    /// </summary>
    public Transform WaypointAlpha;

    /// <summary>
    /// The second Waypoint the NPC will use for his patroling.
    /// </summary>
    public Transform WaypointBeta;

    /// <summary>
    /// The time the NPC should wait at each Waypoint 
    /// before continue walking.
    /// </summary>
    [SerializeField]
    private float TimeToWait = 0.0f;

    /// <summary>
    /// The Timer for that State. Important to get get the time the NPC stay at 
    /// a Waypoint.
    /// </summary>
    private float Timer = 0.0f;

    /// <summary>
    /// Shows wich is the current Wayoint the NPC tries to reach.
    /// </summary>
    private Vector3 targetPosition;

    /// <summary>
    /// Float that shows the distance between the current NPC position and 
    /// the Waypoint he tries to reach.
    /// </summary>
    private float DistanceToWaypoint;

    /// <summary>
    /// Necessary to use the Navigation Mesh for the NPC movement.
    /// </summary>
    NavMeshAgent agent;


    // Use this for initialization
    void Awake()
    {
        // Declares the state
        stateID = StateID.FollowingPath;

        // Get the components of the audio and visual detection and the 
        // NPC Control to use these in the state.
        detectionHear = GetComponent<AudioDetection>();
        detectionSee = GetComponent<VisualDetection>();
        objects = GetComponent<NPCControl>();

        // set the first target Position to Waypoint Alpha's position
        targetPosition = WaypointAlpha.position;


        agent = GetComponent<NavMeshAgent>();

    }

    public override void Reason(GameObject player, GameObject npc)
    {
        // If the NPC heard something he will change to the searching state, no
        // matter if he was walking around or having a break at an Waypoint.
        if (detectionHear.detected)
        {
            agent.speed = 1.0f;
            npc.GetComponent<NPCControl>().SetTransition(Transition.HeardSomething);

        }

        // If the NPC sees the Player he will start chasing him.
        if (detectionSee.detected && objects.SqrDistanceToPlayer > 4)
        {
            npc.GetComponent<NPCControl>().SetTransition(Transition.SawPlayer);
        }
    }

    public override void Act(GameObject player, GameObject npc)
    {
        // Calculate the distance between the the current NPC position and the target Position
        DistanceToWaypoint = (targetPosition - transform.position).sqrMagnitude;

        // if the Distance between the Waypoint and the NPC is under 4 units, a Timer will start
        // and the NPC will slow down until he stands still.
        // If the Time passed the "Time to wait" - border, the NPC will get information about his next 
        // target position, increase his speed and reset the Timer to zero.
        if (DistanceToWaypoint <= 4)
        {
            Timer += Time.deltaTime * 1;
            agent.speed = Mathf.Lerp(agent.speed, 0.0f, 1.0f);

            if (Timer >= TimeToWait)
            {
                ChecktNextDestination();
                agent.speed = 1.0f;
                Timer = 0.0f;
            }
        }

        agent.SetDestination(targetPosition);
    }

    /// <summary>
    /// Check at wich Waypoint the NPC is, and choose his next target position.
    /// </summary>
    private void ChecktNextDestination()
    {
        if (targetPosition == WaypointAlpha.position)
        {
            targetPosition = WaypointBeta.position;
        }
        else
        {
            targetPosition = WaypointAlpha.position;
        }
    }
}
