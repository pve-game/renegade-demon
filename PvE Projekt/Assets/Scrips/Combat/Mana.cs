using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Mana : MonoBehaviour {

    [SerializeField]
    private int currentMana = 1;
    public int CurrentMana { get { return currentMana; } }

    [SerializeField]
    private int maximumMana = 1;
    public int Maximummana { get { return maximumMana; } }

    public bool Alive { get { return currentMana > 0; } }

    public delegate void ManaChanged(float percentage);
    public ManaChanged onManaChanged;

    public void addMana(int value)
    {
        if (onManaChanged != null)
            onManaChanged(currentMana / (float)maximumMana);
    }
}
