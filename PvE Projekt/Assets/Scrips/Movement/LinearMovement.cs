using UnityEngine;
using System.Collections;

public class LinearMovement : MonoBehaviour
{
    /// <summary>
    /// Should the movement take place
    /// </summary>
    [SerializeField]
    private bool active = false;
    public bool Active { get { return active; } set { active = value; } }

    [SerializeField]
    private float speed = 1f;
    public float Speed { get { return speed; } set { speed = Mathf.Max(value, 0f); } }

    /// <summary>
    /// Direction of the movement
    /// </summary>
    private Vector3 direction = Vector3.forward;
    

    // Update is called once per frame
    public void Update()
    {
        if (active)
            transform.Translate(direction * speed * Time.deltaTime , Space.World);
    }
}
