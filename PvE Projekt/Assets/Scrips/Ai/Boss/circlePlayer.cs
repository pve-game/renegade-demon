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

public class circlePlayer : FSMState {

    /// <summary>
    /// Implements the NPC Control to this state.
    /// </summary>
    private NPCControl objects;

	// Use this for initialization
	void Awake () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Reason(GameObject player, GameObject npc)
    {
        throw new System.NotImplementedException();
    }

    public override void Act(GameObject player, GameObject npc)
    {
        throw new System.NotImplementedException();
    }
}
