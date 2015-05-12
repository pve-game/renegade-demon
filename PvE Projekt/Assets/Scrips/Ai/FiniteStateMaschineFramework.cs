using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum Transition
{
    NullTransition,
    LostPlayer,
    SawPlayer,
    HeardSomething,
    Attack,
    A_LostPlayer,
    A_HeardSomething,
    A_SawPlayer,
    B_circumnavigatePlayer,
    B_fastAttack,
    B_escapeAttack,
    B_parry,
}

public enum StateID
{
    NullStateID,
    FollowingPath,
    ChasingPlayer,
    Searching,
    AttackPlayer,
    A_Idle,
    A_Attentive,
    A_Attack,
    B_Circle,
    B_Attack,
    B_EscapeAttack,
    B_Parry,
}

public abstract class FSMState : MonoBehaviour
{
    protected Dictionary<Transition, StateID> map = new Dictionary<Transition, StateID>();
    protected StateID stateID = StateID.NullStateID;
    public StateID ID { get { return stateID; } }

    public void AddTransition(Transition trans, StateID id)
    {
        //Check if any arguments are invalid
        if (trans == Transition.NullTransition)
        {
            Debug.LogError("FSMState ERROR: NullTransitions is not allowed for a real transition");
            return;
        }

        if (id == StateID.NullStateID)
        {
            Debug.LogError("FSMState ERROR: NullStateID is not allowed for a real ID");
            return;
        }

        if (map.ContainsKey(trans))
        {
            Debug.LogError("FSMState ERROR: State " + stateID.ToString() + "already has transition " + trans.ToString() + " Impossible to assign to another state");
            return;
        }
        map.Add(trans, id);
    }

    public void DeleteTransition(Transition trans)
    {
        // Check for NullTransitions
        if (trans == Transition.NullTransition)
        {
            Debug.LogError("FSMState ERROR: NullTransition is not allowed");
            return;
        }

        if (map.ContainsKey(trans))
        {
            map.Remove(trans);
            return;
        }

    }

    public StateID GetOutputState(Transition trans)
    {
        if (map.ContainsKey(trans))
        {
            return map[trans];
        }
        return StateID.NullStateID;
    }

    public virtual void DoBeforeEntering() { }
    public virtual void DoBeforeLeaving() { }
    public abstract void Reason(GameObject player, GameObject npc);
    public abstract void Act(GameObject player, GameObject npc);
}


public class FSMSystem 
{
    private List<FSMState> states;

    private StateID currentStateID;
    public StateID CurrentStateID { get { return currentStateID; } }
    private FSMState currentState;
    public FSMState CurrentState { get { return currentState; } }

    public FSMSystem()
    {
        states = new List<FSMState>();
    }

    public void AddState(FSMState s)
    {
        if ( s == null)
        {
            Debug.LogError("FSM ERROR: Null references are not allowed");
        }

        if (states.Count == 0)
        {
            states.Add(s);
            currentState = s;
            currentStateID = s.ID;
            return;
        }
        else
        {
            foreach (FSMState state in states)
            {
                if (state.ID == s.ID)
                {
                    Debug.LogError("FSM ERROR: Impossible to add state " + s.ID.ToString() + " because state has already been added");
                    return;
                }
            }
        }
        states.Add(s);
    }

    public void DeleteState( StateID id)
    {
        if (id == StateID.NullStateID)
        {
            Debug.LogError("FSM ERROR: NullStateID is not allowed for a real state");
            return;
        }

        foreach (FSMState state in states)
        {
            if(state.ID == id)
            {
                states.Remove(state);
                return;
            }
        }
        Debug.LogError("FSM ERROR: Impossible to delete state " + id.ToString() + ". It was not on the list of states");
    }

    public void PerformTransition ( Transition trans)
    {
        if ( trans == Transition.NullTransition)
        {
            Debug.LogError("FSM ERROR: NullTransition is not allowed for a real transition");
            return;
        }

        StateID id = currentState.GetOutputState(trans);
        if (id == StateID.NullStateID)
        {
            Debug.LogError("FSM Error: State " + currentStateID.ToString() + " does not have a traget state for transition " + trans.ToString());
            return;
        }

        currentStateID = id;
        foreach (FSMState state in states)
        {
            if (state.ID == currentStateID)
            {
                currentState.DoBeforeLeaving();

                currentState = state;

                currentState.DoBeforeEntering();
                break;
            }
        }
    }
}
