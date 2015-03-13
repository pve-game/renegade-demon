using UnityEngine;
using System.Collections;

public class Collect : MonoBehaviour {
   [SerializeField]
    Health health;


	void Start () {
        health = GetComponent<Health>();
	}
	
   void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.layer == LayerMask.NameToLayer("Item"))
       {
           Item item = other.GetComponent<Item>();
           Debug.Log(item.itemType);
           Consume(item);
           GameObject.Destroy(other.gameObject);
       }
    }

   void Consume(Item item)
    {
       switch (item.itemType)
       {
           case Item.Type.Healball:
               Heal();
               break;

           case Item.Type.Key:
               break;
       }
    }

   void Heal()
   {
       health.addHealth(20);
   }
}
