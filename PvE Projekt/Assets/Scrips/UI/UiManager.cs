using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {

    public Image skill_1;
    public Image skill_2;
    public AoE aoe = null;
    public Shot shot = null;
    float timer = 0.0f;

	// Use this for initialization
	void Start () {
        //skill_1 = GetComponent<Image>();
        aoe = GetComponent<AoE>();
	}
	
	// Update is called once per frame
	void Update () {
        Colldown(aoe, skill_1);
        Colldown(shot, skill_2);
    }
   
    void Colldown(Ability ability,Image image)
    {
         if (!ability.Ready)
        {
            timer += Time.deltaTime;
            Debug.Log(ability.Ready);
            if (timer > ability.Cooldown)
            {
                Debug.Log("timerMax reached !");
                timer = 0;
            }
            else
            {
                image.fillAmount = timer / ability.Cooldown;
                Debug.Log("working");
            }
        }
        else
        {
            image.fillAmount = 1.0f;
        }
    }
}
