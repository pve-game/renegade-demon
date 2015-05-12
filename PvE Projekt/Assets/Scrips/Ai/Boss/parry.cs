using UnityEngine;
using System.Collections;

/// <summary>
/// This class represents the parry state of the boss enemy.
/// In this state the boss will do nothing more, then standing still and
/// parry every attack of the player, except attack from higher range.
/// The parry itself is an animation wich is implemented here.
/// </summary>
/// <remarks>
/// Author: Sebastian Borsch
/// </remarks>
/// 
[RequireComponent(typeof(NPCControl))]

public class parry : FSMState {

    private NPCControl _objects;

    private float _timer = 0;
    private float _timeToWait = 10;


	// Use this for initialization
	void Awake () {

        stateID = StateID.B_Parry;

        _objects = GetComponent<NPCControl>();
	}

    public override void Reason(GameObject player, GameObject npc)
    {
        if (_timer > _timeToWait)
        {
            Debug.Log("Aufruf");
            _timer = 0;
            npc.GetComponent<NPCControl>().SetTransition(Transition.B_fastAttack);
        }
    }

    public override void Act(GameObject player, GameObject npc)
    {
        _timer += Time.deltaTime;
        
        // Implementation of hit registreation necessary.
        /*
        if()
        {
            TimeToWait += 1.0f;
        }*/

        Debug.LogWarning("IMPORTANT: Hit registreation neaded");
        
    }
}
