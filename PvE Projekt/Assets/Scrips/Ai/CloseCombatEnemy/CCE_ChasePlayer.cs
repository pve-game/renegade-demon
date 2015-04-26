using UnityEngine;
using System.Collections.Generic;
using UnityEngine;
using System.Text;


[RequireComponent(typeof(NPCControl))]
[RequireComponent(typeof(AudioDetection))]
[RequireComponent(typeof(VisualDetection))]

public class CCE_ChasePlayer : FSMState {

    private VisualDetection detectionSee;
    private AudioDetection detectionHear;
    private NPCControl objects;

    [SerializeField]
    private float chasingSpeed = 0.0f;
    [SerializeField]
    private float AttackDistance;

    NavMeshAgent agent;


	// Use this for initialization
	void Awake () {
        stateID = StateID.ChasingPlayer;
        detectionHear = GetComponent<AudioDetection>();
        detectionSee = GetComponent<VisualDetection>();
        objects = GetComponent<NPCControl>();

        agent = GetComponent<NavMeshAgent>();
	}


    public override void Reason(UnityEngine.GameObject player, UnityEngine.GameObject npc)
    {
        if (!detectionSee.detected && detectionHear.detected)
        {
            npc.GetComponent<NPCControl>().SetTransition(Transition.HeardSomething);
        }

        if (!detectionSee.detected && !detectionHear.detected)
        {
            npc.GetComponent<NPCControl>().SetTransition(Transition.LostPlayer);
        }

        if(objects.Distance <= AttackDistance)
        {
            Debug.Log("Transition to Attack State");
            npc.GetComponent<NPCControl>().SetTransition(Transition.Attack);
        }
    }

    public override void Act(UnityEngine.GameObject player, UnityEngine.GameObject npc)
    {
        Debug.Log(" Ich kriege dich!  ");
        agent.speed = chasingSpeed;

        agent.SetDestination(objects.PlayerPosition.position);
    }


}
