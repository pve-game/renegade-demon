using UnityEngine;
using System.Collections;

public class AudioDetection : MonoBehaviour {

    [SerializeField]
    private Transform Player;
    [SerializeField]
    private float MaxHearDistance;

    public bool detected { get; private set; }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        checkIfPlayerIsAudible(calculateDistance());

    }

    private float calculateDistance()
    {
        float Distance;
        Distance = (Player.position - transform.position).sqrMagnitude;
        return Distance;
    }

    private void checkIfPlayerIsAudible(float Distance)
    {
        if (Distance <= MaxHearDistance)
        {
            Debug.Log("Detected Noise");
            detected = true;
        }
        else
        {
            detected = false;
        }
    }
}
