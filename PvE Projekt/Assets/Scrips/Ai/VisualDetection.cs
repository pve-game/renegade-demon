using UnityEngine;
using System.Collections;

/// <summary>
/// Calculation of the NPC perspective
/// </summary>
/// <remarks>
/// Author: Sebastian Borsch
/// </remarks>
/// 
public class VisualDetection : MonoBehaviour {

    [SerializeField]
    private Transform Player;
    
    /// <summary>
    /// Maximum range of the visibility
    /// </summary>
    [SerializeField]
    private float maxRange;

    /// <summary>
    /// Angle of the visibility
    /// </summary>
    [SerializeField]
    private float angle;


    /// <summary>
    /// Property to call other classes, that something was seen
    /// </summary>
    public bool detected { get; private set; }


    private Vector3 VectorPlayer_Enemy;

	// Update is called once per frame
	void Update () {
        // Every Frame the NPC checks, if someone is visible
        CanSee(calculateDistance());
	}

    /// <summary>
    /// Caluculate how far the Player is from the current Position of the NPC
    /// </summary>
    /// <returns>
    /// Returns the Distance between Player and NPC
    /// </returns>
    private float calculateDistance()
    {
        float Distance;
        Distance = (Player.position - transform.position).sqrMagnitude;
        return Distance;
    }


    /// <summary>
    /// Compare the current Position of the NPC with the 
    /// position of the Player to decide if he is visible for the NPC or not.
    /// </summary>
    /// <param name="Distance">
    /// Needs the calculation of the distance between Player and NPC
    /// </param>
    private void CanSee(float Distance)
    {
        // If the Distance is bigger than the maximum range of visibility the 
        // Player is not visible for the NPC
        if (Distance >= maxRange)
        {
            detected = false;
            return;
        }

        // Calculate the vector from the Player to the NPC
       VectorPlayer_Enemy = Player.position - transform.position;

        // Check if the angle between Player and NPC is small enough that 
        // the Player is visible from the NPC's point of view.
        // If the angle is to big, the Player is invisible for the NPC
         if (Vector3.Angle(transform.forward, VectorPlayer_Enemy) > angle * 0.5)
        {
            detected = false;
            return;
        }

        // The Player is at this point visible for the NPC. Now it is nesseceary 
        // to check if something stands between Player an NPc, for example a wall.
         RaycastHit Hit;
        if (Physics.Raycast(transform.position,VectorPlayer_Enemy,out Hit))
        {
            if (Hit.transform.gameObject == Player.gameObject)
            {
                detected = true;
            }
            else
            {
                detected = false;
            }
        }
    }
}
