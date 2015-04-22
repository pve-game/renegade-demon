using UnityEngine;
using System.Collections;

/// <summary>
/// Calculation of the NPC's auditive range
/// </summary>
/// <remarks>
/// Author: Sebastian Borsch
/// </remarks>
public class AudioDetection : MonoBehaviour {

    [SerializeField]
    private Transform Player;

    /// <summary>
    /// Maximum earshot of the NPC
    /// </summary>
    [SerializeField]
    private float MaxHearDistance;

    /// <summary>
    /// Property to call other classes, if the NPC can hear the Player
    /// </summary>
    public bool detected { get; private set; }

   
    // Update is called once per frame
    void Update()
    {
        // Check every frame if the Player is in earshot of the NPC
        CheckIfPlayerIsAudible(CalculateDistance());

    }

    /// <summary>
    /// Calculate the distance between the Player and the current position of the NPC
    /// </summary>
    /// <returns>
    /// Returns a float value, wich represents the distance between the Player and the NPC
    /// </returns>
    private float CalculateDistance()
    {
        float Distance;
        Distance = (Player.position - transform.position).sqrMagnitude;
        return Distance;
    }

    /// <summary>
    /// Compare the position of the Player with the current NPC position and check
    /// if the Player is audible for the NPC
    /// </summary>
    /// <param name="Distance"></param>
    private void CheckIfPlayerIsAudible(float Distance)
    {
        // check if the Player is insinde earshot of the player
        if (Distance <= MaxHearDistance)
        {
            detected = true;
        }
        else
        {
            detected = false;
        }
    }
}
