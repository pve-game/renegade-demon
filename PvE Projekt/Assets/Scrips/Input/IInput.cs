using UnityEngine;
using System.Collections;

interface IInput
{
    bool UseAttackButton();
    bool UseAoESpellButton();
    bool UseFireSpellButton();
    Vector2 UseKeyboardInput();
    Vector2 UseMouseInput();

}