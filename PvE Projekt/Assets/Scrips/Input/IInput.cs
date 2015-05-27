using UnityEngine;
using System.Collections;

interface IInput
{
    bool UseAttackButton();
    bool UseAoESpellButton();
    bool UseFireSpellButton();
    bool UseSkillmenuButton();
    bool UseRunButton();
    Vector2 UseKeyboardInput();
    Vector2 UseMouseInput();

}