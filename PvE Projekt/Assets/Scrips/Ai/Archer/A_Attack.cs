using UnityEngine;
using System.Collections;

/// <summary>
/// This class represents the attack state of the archer enemy.
/// The archer attacks the target from the distance and walks around 
/// to make it more difficult for the Player to shot him.
/// </summary>
/// <remarks>
/// Author: Sebastian Borsch
/// </remarks>

[RequireComponent(typeof(NPCControl))]
[RequireComponent(typeof(VisualDetection))]
[RequireComponent(typeof(AudioDetection))]

public class A_Attack : FSMState
{

    /// <summary>
    /// Implemets the audio detection to this state.
    /// </summary>
    private AudioDetection detectionHear;

    /// <summary>
    /// Implemets the visual detetcion to this state.
    /// </summary>
    private VisualDetection detectionSee;

    /// <summary>
    /// Implements the NPC Control to this state.
    /// </summary>
    private NPCControl objects;

    /// <summary>
    /// Necessary to use the Navigation Mesh for the NPC movement.
    /// </summary>
    NavMeshAgent agent;

    private bool isMoving = false;
    private Vector3 newPosition;

    [SerializeField]
    private float attackDistance;

    [SerializeField]
    private float prancingDistance;

    [SerializeField]
    private float timeToWalk = 2.0f;

    private float time = 0.0f;

    private Vector3 oldPosition;

    // Use this for initialization
    void Awake()
    {
        stateID = StateID.A_Attack;

        // Get the components of the audio and visual detection an the
        // NPC Control to use this in the state.
        detectionHear = GetComponent<AudioDetection>();
        detectionSee = GetComponent<VisualDetection>();
        objects = GetComponent<NPCControl>();

        agent = GetComponent<NavMeshAgent>();

        
        agent.speed = 4.0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Reason(GameObject player, GameObject npc)
    {
        if (!detectionSee.detected && detectionHear.detected)
        {
            agent.Stop();
            npc.GetComponent<NPCControl>().SetTransition(Transition.A_HeardSomething);
        }

        if (!detectionHear.detected && !detectionSee.detected)
        {
            npc.GetComponent<NPCControl>().SetTransition(Transition.A_LostPlayer);
        }
    }

    public override void Act(GameObject player, GameObject npc)
    {
        if (objects.SqrDistanceToPlayer >= attackDistance)
        {
           // Debug.Log("attackZone");
            shoot();
        }
        else if (objects.SqrDistanceToPlayer <= attackDistance && objects.SqrDistanceToPlayer >= prancingDistance)
        {
           // Debug.Log("prancingZone");
            if (!isMoving)
            {
                setNewWalkDirection(randomStepDecision());
                isMoving = true;
            }
            GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(oldPosition, newPosition, time / timeToWalk));
            time += Time.deltaTime;

           if(time > 2)
           {
               isMoving = false;
               time = 0.0f;
           }
            transform.LookAt(objects.PlayerPosition);
            
            shoot();
        }
        else if (objects.SqrDistanceToPlayer <= prancingDistance)
        {
            // Gummibaum
        }

    }

    /// <summary>
    /// The Archer will shot an arrow to the enemy
    /// </summary>
    private void shoot()
    {
        Debug.Log("Shoot  //  Attack from Distance!");
    }

    /// <summary>
    /// Produce a random value, which decides in wich direction the archer should walk.
    /// The Decision is given as an integer. 1 means left and 2 means right. Value 0 means that the Archer
    /// sould stand still.
    /// </summary>
    /// <returns>
    /// new Direction as an integer value.
    /// </returns>
    private int randomStepDecision()
    {
        int newDirection;

        float value = Random.value;

        if (value < 0.5f)
        {
            newDirection = 1;
        }
        else if (value > 0.5f)
        {
            newDirection = 2;
        }
        else
        {
            newDirection = 0;
        }
        return newDirection;
    }

    private void setNewWalkDirection(int newDirection)
    {
        Vector3 walkLeft = new Vector3(-4.0f, 0.0f, 0.0f);
        Vector3 walkRight = new Vector3(4.0f, 0.0f, 0.0f);

        oldPosition = transform.position;

        switch (newDirection)
        {
            case 1:
                newPosition = transform.position - transform.right * 4;
                break;
            case 2:
                newPosition = transform.position + transform.right * 4;
                break;
            default:
                newPosition = transform.position;
                break;
        }
            //agent.SetDestination(newPosition);
            Debug.Log("new Position: " + newPosition);
            
    }

    private void checkIfArcherIsAtDestination()
    {
        float DistanceToDestination;

        DistanceToDestination = (newPosition - transform.position).sqrMagnitude;

        Debug.Log("Distance to destination: " + DistanceToDestination);
    }
}
