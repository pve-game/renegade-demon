using UnityEngine;
using System.Collections;


public class Item:MonoBehaviour
{
    public enum Type
    {
        Healball,
        Key,
        None
    }
   public Type itemType = Type.None;

}