using UnityEngine;
using System.Collections;

public class VisualDetection : MonoBehaviour {

    [SerializeField]
    private Transform Player;
    [SerializeField]
    private float maxRange;
    [SerializeField]
    private float angle;

    public bool detected { get; private set; }

    private Vector3 VectorPlayer_Enemy;

	// Use this for initialization
	void Awake () {
	
	}
	
	// Update is called once per frame
	void Update () {
        CanSee(calculateDistance());
	}

    private float calculateDistance()
    {
        float Distance;
        Distance = (Player.position - transform.position).sqrMagnitude;
        return Distance;
    }

    private void CanSee(float Distance)
    {
        if (Distance >= maxRange)
        {
            detected = false;
            return;
        }

       VectorPlayer_Enemy = Player.position - transform.position;

         if (Vector3.Angle(transform.forward, VectorPlayer_Enemy) > angle * 0.5)
        {
            detected = false;
            return;
        }

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
