using UnityEngine;
using System.Collections;

public class Status : MonoBehaviour {

   public enum ObjectStatus
    {
        Stunned,
        Running,
        Idle,
        Healing,
        None
    }

    public ObjectStatus status = ObjectStatus.None;
}
