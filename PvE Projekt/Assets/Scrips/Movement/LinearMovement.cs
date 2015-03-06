using UnityEngine;
using System.Collections;

public class LinearMovement : MonoBehaviour
{
    /// <summary>
    /// Type for movement completion
    /// </summary>
    public delegate void MovementFinished();
    private MovementFinished movementCompleted;
    public MovementFinished OnMovementCompleted { get { return movementCompleted; } }
    /// <summary>
    /// Should the movement take place
    /// </summary>
    [SerializeField]
    private bool active = false;
    public bool Active { get { return active; } set { active = value; } }

    /// <summary>
    /// Velocity of the movement
    /// </summary>
    [SerializeField]
    private float speed = 1f;
    public float Speed { get { return speed; } set { speed = Mathf.Max(value, 0f); } }

    /// <summary>
    /// Direction of the movement
    /// </summary>
    private Vector3 direction = Vector3.forward;
    public Vector3 Direction { get { return direction; } set { direction = value; } }

    private float percentage = 0f;
    private Vector3 start = Vector3.zero;
    private Vector3 destination = Vector3.zero;

    // Update is called once per frame
    public void Update()
    {
        if (active)
        {
            transform.position = Vector3.Lerp(start, destination, percentage);
            percentage += speed * Time.deltaTime;
            if (percentage > 1f)
            {
                if (movementCompleted != null)
                    movementCompleted();
                active = false;
            }
        }
    }

    /// <summary>
    /// initalizes start and end position of the movement and activates the effect
    /// </summary>
    /// <param name="begin">start position</param>
    /// <param name="end">target position</param>
    public void StartMovement(Vector3 begin, Vector3 end)
    {
        start = begin;
        destination = end;
        active = true;
        percentage = 0f;
    }



}
