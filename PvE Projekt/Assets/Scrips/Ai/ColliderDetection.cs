using UnityEngine;
using System.Collections;

public class ColliderDetection : MonoBehaviour
{
   [HideInInspector]
   public bool seen = false;
    [HideInInspector]
   public bool heard = false;
    // Use this for initialization
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnTriggerStay(Collider other)
    {
       if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

            if (gameObject.layer == LayerMask.NameToLayer("See"))
            {
                seen = true;
                Debug.Log("Saw you, Player!");
            }

            if (gameObject.layer == LayerMask.NameToLayer("Hear"))
            {
                heard = true;
                Debug.Log("Heard you, Player!");
            }
        }
       
    }
}
