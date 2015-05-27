using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class InputManager : MonoBehaviour
{

    IInput input = null;
    Move move = null;
    AoE aoe = null;
    Shot shot = null;
    UiManager ui = null;
    Animator anim = null;

    [SerializeField]
    Canvas newCanvas;

    // Use this for initialization
    void Start()
    {
        input = new InputKeyboard();
        move = GetComponent<Move>();
        aoe = GetComponent<AoE>();
        shot = GetComponent<Shot>();
        ui = GetComponent<UiManager>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (input.UseKeyboardInput() != Vector2.zero)
        {
            Movement();
        }

        if (input.UseAoESpellButton())
        {
            aoe.Use();
        }

        if (input.UseFireSpellButton())
        {
            shot.Use();
            anim.SetBool("useMagic", true);
        }
        if (input.UseSkillmenuButton())
        {
            ui.ChangeCanvas(newCanvas);
        }
        if(input.UseRunButton())
        {
            move.Run = true;
        }
        if (!input.UseRunButton())
        {
            move.Run = false;
        }

    }

    void Movement()
    {
        move.H = input.UseKeyboardInput().x;
        move.V = input.UseKeyboardInput().y;
    }
}
