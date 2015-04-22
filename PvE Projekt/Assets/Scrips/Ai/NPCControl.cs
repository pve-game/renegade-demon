using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;


public class NPCControl : MonoBehaviour {

    GameObject player;
    private FSMSystem fsm;
    [SerializeField]
    private Transform _player;
    public Transform PlayerPosition { get { return _player; } }

    private float _distance;
    public float Distance { get { return _distance; } }

    public void SetTransition(Transition t) { fsm.PerformTransition(t); }

    void Start()
    {
        MakeFSM();
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

    private void MakeFSM()
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
}