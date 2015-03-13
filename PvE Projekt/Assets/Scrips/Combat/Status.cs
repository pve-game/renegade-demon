using UnityEngine;
using System.Collections;

public class Status : MonoBehaviour {

   public enum ObjectStatus
    {
        Stunned,
        Runing,
        Idel,
        Healing,
        None
    }

    public ObjectStatus status = ObjectStatus.None;
}
