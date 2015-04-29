using UnityEngine;
using System.Collections;



[RequireComponent(typeof(NPCControl))]
[RequireComponent(typeof(AudioDetection))]
[RequireComponent(typeof(VisualDetection))]

public class CCE_Search : FSMState {

    private AudioDetection detectionHear;
    private VisualDetection detectionSee;
    private NPCControl objects;

       private Vector3 playerPosition;

   private bool searching;

   private float DistanceToAudioSource;

    private float Timer = 0.0f;
    private float TimeToLookAround = 2.0f;

    NavMeshAgent agent;

	// Use this for initialization
	void Awake () {
        stateID = StateID.Searching;

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
        
        if (!searching && !detectionHear.detected)
        {
            npc.GetComponent<NPCControl>().SetTransition(Transition.LostPlayer);
        }

        if (detectionSee.detected && objects.DistanceToPlayer > 25)
        {
            npc.GetComponent<NPCControl>().SetTransition(Transition.SawPlayer);
        }
    }

    public override void Act(GameObject player, GameObject npc)
    {

        TimeCounter();
        DistanceToAudioSource = Vector3.SqrMagnitude(playerPosition - transform.position);
        //Debug.Log("Distance: " + Distance);
        if (detectionHear.detected)
        {
            playerPosition = objects.PlayerPosition.position;
            agent.SetDestination(playerPosition);
            searching = true;
        }

        if(DistanceToAudioSource <= 2)
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


        if(Timer >= TimeToLookAround)
        {
            agent.speed = 1.0f;
            Timer = 0.0f;
            
        }
    }
}
