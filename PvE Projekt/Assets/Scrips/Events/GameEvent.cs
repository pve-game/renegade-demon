using UnityEngine;
using System.Collections;
using System;

public class GameEvent : EventArgs
{

	protected GameObject sender = null;
    public GameObject Sender { get { return sender; } }
    public GameEvent(GameObject sender) : base()
    {
        this.sender = sender;
    }
}
