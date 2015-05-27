using UnityEngine;
using System.Collections;

public class InputKeyboard : IInput {

    public Vector2 UseKeyboardInput()
    {
        Vector2 movementDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        return movementDir;
    }
    public Vector2 UseMouseInput()
    {
        Vector2 mouseDir = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        return mouseDir;
    }
    public bool UseAoESpellButton()
    {
        bool Use = Input.GetButtonDown("AoESpell");
        return Use;
        
    }
    public bool UseFireSpellButton()
    {
        bool Use = Input.GetButtonDown("FireSpell");
        return Use;
    }
    public bool UseAttackButton()
    {
        bool Use = Input.GetButtonDown("Fire1");
        return Use;
    }

    public bool UseSkillmenuButton()
    {
        bool Use = Input.GetButtonDown("Menu");
        return Use;
    }
    public bool UseRunButton()
    {
        bool Use = Input.GetButton("Run");
        return Use;
    }
}
