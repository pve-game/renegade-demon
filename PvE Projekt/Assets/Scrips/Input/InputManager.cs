using UnityEngine;
using System.Collections;


public class InputManager : MonoBehaviour {

    IInput input = null;
    Move move = null;
    AoE aoe = null;
    Shot shot = null;

	// Use this for initialization
	void Start () {
        input = new InputKeyboard();
        move = GetComponent<Move>();
        aoe = GetComponent<AoE>();
        shot = GetComponent<Shot>();
	}
	
	// Update is called once per frame
	void Update () {
	if(input.UseKeyboardInput() != Vector2.zero)
    {
        Movement();
    }
    if (input.UseAoESpellButton())
    {
        aoe.Use();
    }
    if(input.UseFireSpellButton())
    {
        shot.Use();
    }

	}

    void Movement()
    {
        move.Movement(input.UseKeyboardInput().x, input.UseKeyboardInput().y);
    }
}
