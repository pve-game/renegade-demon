using UnityEngine;
using System.Collections;

/// <summary>
/// This class represents the idle state of the boss enemy. THe boss circles the player
/// and waits for the right moment to attack him.
/// </summary>
/// <remarks>
/// Author: Sebastian Borsch
/// </remarks>

[RequireComponent(typeof(NPCControl))]

public class circlePlayer : FSMState
{

    /// <summary>
    /// Implements the NPC Control to this state.
    /// </summary>
    private NPCControl objects;

    private Vector3 newPosition = new Vector3(2.0f, 0.0f, 1.0f);

    // Use this for initialization
    void Awake()
    {
        stateID = StateID.B_Circle;

        objects = GetComponent<NPCControl>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Reason(GameObject player, GameObject npc)
    {
        Debug.LogWarning("Not implemented so far");

    }

    public override void Act(GameObject player, GameObject npc)
    {
        Debug.LogWarning("Not implemented so far");
        /*transform.position += Vector3.right * Time.deltaTime;
        transform.position += Vector3.forward * Time.deltaTime;
        transform.LookAt(objects.PlayerPosition);*/

    }
}
