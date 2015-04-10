using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

/// <summary>
/// Container for talent selection
/// </summary>
/// <remarks>
/// Author: Martin Wettig
/// </remarks>
public class TalentBook : MonoBehaviour
{
    /// <summary>
    /// Root canvas for all gui elements
    /// </summary>
    [SerializeField]
    private Canvas uiCanvas = null;

    /// <summary>
    /// Talent book UI canvas
    /// </summary>
    [SerializeField]
    private Canvas bookCanvas = null;
    /// <summary>
    /// Visual style of the talent buttons
    /// </summary>
    [SerializeField]
    private Toggle buttonPrefab;

    /// <summary>
    /// Number of available talent points if no talent is learned
    /// </summary>
    [SerializeField]
    private int maximumTalentPoints = 2;
    /// <summary>
    /// Currently available talent points
    /// </summary>
    private int talentPoints = 0;
    public int TalentPoints { get { return talentPoints; } }
    /// <summary>
    /// Talents from which the user can choose
    /// </summary>
    private List<Talent> talents = null;
    /// <summary>
    /// Collection of talent toggle buttons, i.e. the visual representation
    /// </summary>
    private List<Toggle> talentUIelements = null;
    
    public void Awake()
    {
        talents = new List<Talent>();
        talentPoints = maximumTalentPoints;
        talentUIelements = new List<Toggle>();
    }

    public void AddTalent(Talent t)
    {
        //save the talent in the book
        talents.Add(t);
        Toggle talentToggleButton = CreateTalentButton(t);
        talentUIelements.Add(talentToggleButton);
    }

    private Toggle CreateTalentButton(Talent t)
    {
        //create a ui element to be able to choose a talent
        Toggle talentToggleButton = Instantiate(buttonPrefab) as Toggle;
        talentToggleButton.GetComponentInChildren<Text>().text = t.TalentName;
        //parent toggle to talent ui canvas and adjust its position
        //relative to the parent
        talentToggleButton.transform.SetParent(bookCanvas.transform, false);
        //apply talent icon to the button
        talentToggleButton.image.sprite = t.Icon;

        //button transform
        RectTransform talentRectTransform = talentToggleButton.GetComponent<RectTransform>();
        //prepare a vector for the local position of the button
        Vector3 buttonPosition = new Vector3(-60f, 0f, 0f);
        buttonPosition.y = 120f //topmost position
        - talentUIelements.Count * (talentRectTransform.rect.height + 15f); // vertical offset including margin
            

        talentRectTransform.localPosition = buttonPosition;
        Vector3 tooltipPosition = talentRectTransform.TransformVector(talentRectTransform.localPosition);
        //place the tooltip beside the talent
        RectTransform ttrt = t.Tooltip.GetComponent<RectTransform>();
        //ttrt.transform.SetParent(bookCanvas.transform, false);
        buttonPosition.x = -30f;
        //236,273 -> targetpos
        ttrt.position = uiCanvas.transform.TransformVector(new Vector3(236f, 273f, 0f));
            //tooltipPosition;
        //connect OnEnter and OnLeave events 
        //with displaying or hiding the tooltip
        EventTrigger et = talentToggleButton.GetComponent<EventTrigger>();
        for (int i = 0; i < et.delegates.Count; i++)
        {
            EventTrigger.Entry e = et.delegates[i];
            if (e.eventID == EventTriggerType.PointerEnter)
            {
                //pass event data by lambda expression
                e.callback.AddListener((eventdata) => {  t.ShowTooltip(); });
            }
            else if (e.eventID == EventTriggerType.PointerExit)
            {
                e.callback.AddListener((eventdata) => { t.HideTooltip(); });
            }
            //else if (e.eventID == EventTriggerType.PointerClick)
            //{
            //    e.callback.AddListener(new UnityAction<BaseEventData>(foo));
            //}
        }

        talentToggleButton.onValueChanged.AddListener(t.SetState);
        return talentToggleButton;
    }

    //todo: stat point check prior to learning
    //todo: change to click event instead of value change to get the sending ui element

    /// <summary>
    /// Consumes a talent point during learning
    /// </summary>
    public void LearnTalent()
    {
        talentPoints = Mathf.Clamp(--talentPoints, 0, maximumTalentPoints);
    }

    /// <summary>
    /// Checks if there are talent points left to spent
    /// </summary>
    /// <returns>true if talent points are available</returns>
    public bool CanLearn()
    {
        return talentPoints > 0;
    }
}
