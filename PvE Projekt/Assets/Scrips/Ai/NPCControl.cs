using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;




/// <summary>
/// All types of NPC are stored here.
/// </summary>
/// 

enum NPCTypes
{
    CloseCombat,
    Archer,
    Boss,
}

public class NPCControl : MonoBehaviour {

    GameObject player;
    private FSMSystem fsm;
    [SerializeField]
    private Transform _player;
    public Transform PlayerPosition { get { return _player; } }

    private float _distance;
    public float DistanceToPlayer { get { return _distance; } }

    [SerializeField]
    private NPCTypes types;

    public void SetTransition(Transition t) { fsm.PerformTransition(t); }

    void Start()
    {
        switch (types)
        {
            case NPCTypes.CloseCombat:
                MakeFSM_CCE();
                break;
            case NPCTypes.Archer:
                MakeFSM_Archer();
                break;
            case NPCTypes.Boss:
                MakeFSM_Boss();
                break;
            default:
                break;
        }
       
    }    

    void Update()
    {
        _distance = (PlayerPosition.position - transform.position).sqrMagnitude;
    }

	void LateUpdate () 
    {
		Debug.Log(fsm.CurrentState);
        fsm.CurrentState.Reason(player, gameObject);
        fsm.CurrentState.Act(player, gameObject);
	}

    /// <summary>
    /// Builds the finite state maschine for the close combat enemy.
    /// </summary>
    private void MakeFSM_CCE()
    {
        CCE_Patrol followPath = GetComponent<CCE_Patrol>();
        followPath.AddTransition(Transition.HeardSomething, StateID.Searching);
        followPath.AddTransition(Transition.SawPlayer, StateID.ChasingPlayer);
        followPath.AddTransition(Transition.Attack, StateID.AttackPlayer);

        CCE_Search search = GetComponent<CCE_Search>();
        search.AddTransition(Transition.LostPlayer, StateID.FollowingPath);
        search.AddTransition(Transition.SawPlayer, StateID.ChasingPlayer);
        search.AddTransition(Transition.Attack, StateID.AttackPlayer);

        CCE_ChasePlayer chase = GetComponent<CCE_ChasePlayer>();
        chase.AddTransition(Transition.HeardSomething, StateID.Searching);
        chase.AddTransition(Transition.LostPlayer, StateID.FollowingPath);
        chase.AddTransition(Transition.Attack, StateID.AttackPlayer);

        CCE_Attack attack = GetComponent<CCE_Attack>();
        attack.AddTransition(Transition.SawPlayer, StateID.ChasingPlayer);
        

        fsm = new FSMSystem();
        fsm.AddState(followPath);
        fsm.AddState(search);
        fsm.AddState(chase);
        fsm.AddState(attack);
    }

    /// <summary>
    /// Builds the finite state maschine for the archer enemy
    /// </summary>
    private void MakeFSM_Archer()
    {
        A_Idle idle = GetComponent<A_Idle>();
        idle.AddTransition(Transition.A_HeardSomething, StateID.A_Attentive);
        idle.AddTransition(Transition.A_SawPlayer, StateID.A_Attack);

        A_Attentive attentive = GetComponent<A_Attentive>();
        attentive.AddTransition(Transition.A_LostPlayer, StateID.A_Idle);
        attentive.AddTransition(Transition.A_SawPlayer, StateID.A_Attack);

        A_Attack distanceAttack = GetComponent<A_Attack>();
        distanceAttack.AddTransition(Transition.A_LostPlayer, StateID.A_Idle);
        distanceAttack.AddTransition(Transition.A_HeardSomething, StateID.A_Attentive);


        fsm = new FSMSystem();
        fsm.AddState(idle);
        fsm.AddState(attentive);
        fsm.AddState(distanceAttack);
    }

    /// <summary>
    /// Builds the finite state maschine for the boss enemy.
    /// </summary>
    private void MakeFSM_Boss()
    {
        Debug.LogError("This FSM is not implemented yet");
    }
}