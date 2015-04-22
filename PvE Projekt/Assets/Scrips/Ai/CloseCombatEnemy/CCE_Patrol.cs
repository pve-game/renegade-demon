using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NPCControl))]
[RequireComponent(typeof(VisualDetection))]
[RequireComponent(typeof(AudioDetection))]

public class CCE_Patrol : FSMState {

   private AudioDetection detectionHear;
   private VisualDetection detectionSee;
   private NPCControl objects;

   public Transform WaypointAlpha;
   public Transform WaypointBeta;

   public float TimeToWait = 0.0f;
   private float Timer = 0.0f;
   private float currentTime;

   private Vector3 targetPosition;

   private float DistanceToWaypoint;

   NavMeshAgent agent;


	// Use this for initialization
	void Awake () 
    {
        stateID = StateID.FollowingPath;
        detectionHear = GetComponent<AudioDetection>();
        detectionSee = GetComponent<VisualDetection>();
        objects = GetComponent<NPCControl>();
        targetPosition = WaypointAlpha.position;
        //yRotation = transform.rotation;
        agent = GetComponent<NavMeshAgent>();
       
	}

    public override void Reason(GameObject player, GameObject npc)
    {
        if (detectionHear.detected)
        {
            agent.speed = 1.0f;
            npc.GetComponent<NPCControl>().SetTransition(Transition.HeardSomething);
           
        }
        if (detectionSee.detected && objects.Distance > 4)
        {
            npc.GetComponent<NPCControl>().SetTransition(Transition.SawPlayer);
        }
    }

    public override void Act(GameObject player, GameObject npc)
    {
        DistanceToWaypoint = (targetPosition - transform.position).sqrMagnitude;
        
       
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
