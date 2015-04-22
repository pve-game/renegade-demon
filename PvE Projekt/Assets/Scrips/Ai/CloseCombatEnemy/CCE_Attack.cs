using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NPCControl))]
[RequireComponent(typeof(VisualDetection))]
[RequireComponent(typeof(AudioDetection))]

public class CCE_Attack : FSMState {

    private NPCControl objects;

    private float Distance;
    [SerializeField]
    private float TransitionDistance;


	// Use this for initialization
	void Awake () {
        stateID = StateID.AttackPlayer;
        objects = GetComponent<NPCControl>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}



    public override void Reason(GameObject player, GameObject npc)
    {
       if (objects.Distance > TransitionDistance)
       {
           npc.GetComponent<NPCControl>().SetTransition(Transition.SawPlayer);
       }
    }

    public override void Act(GameObject player, GameObject npc)
    {
        Debug.Log("Attack!");
    }
}
