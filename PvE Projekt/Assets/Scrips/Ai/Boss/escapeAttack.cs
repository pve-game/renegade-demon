using UnityEngine;
using System.Collections;

/// <summary>
/// This class represents the escape attack of the boss enemy. This state gets called,
/// when the boss tries to get a little bit more distance between him and the player.
/// </summary>
/// <remarks>
/// Author: Sebastian Borsch
/// </remarks>


[RequireComponent(typeof(NPCControl))]
[RequireComponent(typeof(Rigidbody))]

public class escapeAttack : FSMState
{
    private NPCControl _objects;

    [SerializeField]
    private float _newDistanceToPlayer = 10;
    private float _timer;

    // Use this for initialization
    void Awake()
    {
        stateID = StateID.B_EscapeAttack;

        _objects = GetComponent<NPCControl>();

    }

    public override void Reason(GameObject player, GameObject npc)
    {
        if(_objects.SqrDistanceToPlayer > _newDistanceToPlayer)
        {
            npc.GetComponent<NPCControl>().SetTransition(Transition.B_fastAttack);
        }
    }

    public override void Act(GameObject player, GameObject npc)
    {
        _timer += Time.deltaTime;

        GoToNewPosition();
    }

    private void GoToNewPosition()
    {

        if (_objects.SqrDistanceToPlayer < _newDistanceToPlayer)
        {
            if (Physics.Raycast(transform.position, -transform.forward, _newDistanceToPlayer))
            {
                GetComponent<Rigidbody>().MovePosition(transform.position - transform.forward * 0.3f);
            }
            else if (Physics.Raycast(transform.position, transform.right, _newDistanceToPlayer))
            {
                GetComponent<Rigidbody>().MovePosition(transform.position + transform.right * 0.3f);
            }
            else if (Physics.Raycast(transform.position, -transform.right, _newDistanceToPlayer))
            {
                GetComponent<Rigidbody>().MovePosition(transform.position - transform.right * 0.3f);
            }
        }
        else
        {
            _timer = 0.0f;
        }
    }


}
