using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {
   
    [SerializeField]
    Image skill_1;
    [SerializeField]
    Image skill_2;
    [SerializeField]
    Image healthBar;
    [SerializeField]
    Image manaBar;
    [SerializeField]
    Canvas uiCanvas;
    [SerializeField]
    Canvas archerCanvas;
    [SerializeField]
    Canvas skillCanvas;
    [SerializeField]
    Canvas actuallCanvas;

   

    AoE aoe;
    Shot shot;
    Health health;
    Mana mana;

    float timer = 0.0f;

	void Start () {

        shot = GetComponent<Shot>();
        aoe = GetComponent<AoE>();
        health = GetComponent<Health>();
        mana = GetComponent<Mana>();
	}
	
	// Update is called once per frame
	void Update () {
        Colldown(aoe, skill_1);
        Colldown(shot, skill_2);
        ChangeHealthBar(health, healthBar);
    }
   

    void ChangeHealthBar(Health health, Image image)
    {
        image.fillAmount = health.CurrentHealth / health.MaximumHealth;
    }

    void ChangeManaBar(Mana mana, Image image)
    {
        image.fillAmount = mana.CurrentMana / mana.Maximummana;
    }

    void Colldown(Ability ability,Image image)
    {
         if (!ability.Ready)
        {
            timer += Time.deltaTime;
            if (timer > ability.Cooldown)
            {
                timer = 0;
            }
            else
            {
                image.fillAmount = timer / ability.Cooldown;
            }
        }
        else
        {
            image.fillAmount = 1.0f;
        }
    }

    public void ChangeCanvas(Canvas canvas)
    {
        actuallCanvas = canvas; 
    }


}
