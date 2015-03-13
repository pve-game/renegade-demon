using UnityEngine;
using System.Collections;

public class OrbitingMovement : MonoBehaviour
{
    /// <summary>
    /// Point around which the movement takes place
    /// </summary>
    [SerializeField]
    private Transform center = null;
    public Transform Center { get { return center; } set { center = value; } }

    /// <summary>
    /// Orbiting distance from the center
    /// </summary>
    [SerializeField]
    private float distance = 1.0f;
    public float Distance { get { return distance; } set { distance = Mathf.Max(value, 0f); } }

    private bool isActive = false;
    public bool IsActive { get { return isActive; } }

    /// <summary>
    /// Movement speed
    /// </summary>
    [SerializeField]
    private float speed = 20f;
    public float Speed { get { return speed; } set { speed = Mathf.Max(value, 0f); } }
    /// <summary>
    /// Time until movement termination
    /// </summary>
    [SerializeField]
    private float duration = 20f;
    public float Duration { get { return duration; } set { duration = Mathf.Max(value, 0f); } }

    private float startTime = 0f;
    private Vector3 point = Vector3.zero;
    // Update is called once per frame
    void Update()
    {
        //if the movement should be applies and its application time is lower than the specified duration
        if (isActive && (Time.time - startTime) < duration)
        {
            transform.RotateAround(center.position, Vector3.up, speed * Time.deltaTime);
            //from unity answers
//             orbitDesiredPosition = (thisTransform.position - objectToOrbit.position).normalized * orbitRadius + objectToOrbit.position;
//thisTransform.position = Vector3.Slerp(thisTransform.position, orbitDesiredPosition, Time.deltaTime * orbitRadiusCorrectionSpeed)
        }
        else
        {
            isActive = false;
        }
    }

    public void StartMovement(Transform center)
    {
        this.center = center;
        startTime = Time.time;
        isActive = true;
    }

}
