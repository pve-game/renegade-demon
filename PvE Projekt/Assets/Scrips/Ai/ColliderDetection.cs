using UnityEngine;
using System.Collections;

public class ColliderDetection : MonoBehaviour
{

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
        //Debug.Log(LayerMask.LayerToName(other.gameObject.layer));
        //if(other.gameObject.layer == LayerMask.NameToLayer("see"))
        //{
        //    if (other.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
        //        Debug.Log("Saw you, Player!");
        //}
        
        //if(other.gameObject.layer == LayerMask.NameToLayer("hear"))
        //{
        //     if (other.transform.gameObject.layer == LayerMask.NameToLayer("Player")) 
        //        Debug.Log("Heard you, Player!");
        //}

        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

            if (gameObject.layer == LayerMask.NameToLayer("see"))
                Debug.Log("Saw you, Player!");
            if (gameObject.layer == LayerMask.NameToLayer("hear"))
                Debug.Log("Heard you, Player!");
        }
       
    }
}
