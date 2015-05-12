using UnityEngine;
using System.Collections;


enum Steps
{
    Step_GetCloseToPlayer,
    Step_AtTargetPosition,
    Step_AnimationDone,
    Step_AttackStateDone,
}

/// <summary>
/// This calss represents the fast attack state of the boss enemey.
/// In this state, the boss will jump forward to the player, hit him 
/// two times and get a little bit more distance between the player and him.
/// </summary>
/// <remarks>
/// Author: Sebastian Borsch
/// </remarks>
/// 

[RequireComponent(typeof(NPCControl))]
[RequireComponent(typeof(Rigidbody))]


public class fastAttack : FSMState
{
    private NPCControl _objects;
    NavMeshAgent _agent;

    Steps _currentStep = Steps.Step_GetCloseToPlayer;

    [SerializeField]
    private float _attackDistance = 5.0f;

    private int _call = 0;


    // Use this for initialization
    void Awake()
    {

        stateID = StateID.B_Attack;

        _objects = GetComponent<NPCControl>();
        _agent = GetComponent<NavMeshAgent>();

    }

    public override void Reason(GameObject player, GameObject npc)
    {
        if (_currentStep == Steps.Step_AttackStateDone)
        {
            Debug.Log("Aufruf attack state");
            Debug.Log(_call);
            switch (_call)
            {
                case 0:
                    _call++;
                    npc.GetComponent<NPCControl>().SetTransition(Transition.B_escapeAttack);
                    break;
                case 1:
                    _call = 0;
                    npc.GetComponent<NPCControl>().SetTransition(Transition.B_parry);
                    break;
                default:
                    Debug.LogError("BossAI ERROR fastAttack: Transition is not available");
                    break;
            }

        }
    }

    public override void Act(GameObject player, GameObject npc)
    {
        GetCloseToPlayer();
        if (_currentStep == Steps.Step_AtTargetPosition)
        {
            PlayAnimation();
        }
        if (_currentStep == Steps.Step_AnimationDone)
        {
            GetMoreDistanceToPlayer();
        }

    }

    /// <summary>
    /// The Boss looks for the current position of the player and runs to him, but
    /// stops with enough distance to hit him directly.
    /// </summary>
    private void GetCloseToPlayer()
    {

        if (_objects.SqrDistanceToPlayer > _attackDistance)
        {
            _agent.Resume();
            _agent.SetDestination(_objects.PlayerPosition.position);
        }

        if (_objects.SqrDistanceToPlayer <= _attackDistance)
        {
            _currentStep = Steps.Step_AtTargetPosition;
        }

    }

    private void PlayAnimation()
    {
        Debug.Log("Play attack animation one. Not implemented so far!");
        Debug.Log("Play attack animation two. Not implemented so far!");

        _currentStep = Steps.Step_AnimationDone;
        
    }

    /// <summary>
    /// After hitting the Player, the boss gets a little distance between him and the 
    /// player.
    /// </summary>
    private void GetMoreDistanceToPlayer()
    {
        if (_objects.SqrDistanceToPlayer < _attackDistance * 3)
        {
            GetComponent<Rigidbody>().MovePosition(transform.position - transform.forward * 0.3f);
        }
        else
        {
            _agent.Stop();
            _currentStep = Steps.Step_AttackStateDone;
            
        }
    }
}
