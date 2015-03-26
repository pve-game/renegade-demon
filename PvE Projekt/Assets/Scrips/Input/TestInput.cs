using UnityEngine;
using System.Collections;

public class TestInput : MonoBehaviour {

    public Shot shot;
    public AoE aoe;
    public Move move;

	// Use this for initialization
	void Start () {
        shot = GetComponent<Shot>();
        aoe = GetComponent<AoE>();
        move = GetComponent<Move>();
	}
	
	// Update is called once per frame
	void Update () {
        OnPress();

	}

    void OnPress()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            shot.Use();
 
        }
        if(Input.GetButtonDown("Fire2"))
        {
            aoe.Use();
        }
    }
}
