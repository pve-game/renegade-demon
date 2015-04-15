using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/// <summary>
/// Additional feature a player can choose
/// </summary>
/// <remarks>
/// Author: Martin Wettig
/// </remarks>
[RequireComponent(typeof(TalentBook))]
public abstract class Talent : MonoBehaviour
{
    [SerializeField]
    private string talentName = "";
    public string TalentName { get { return talentName; } }

    [SerializeField]
    private string description = "";
    public string Description { get { return description; } }

    [SerializeField]
    private Sprite icon = null;
    public Sprite Icon { get { return icon; } }


    [SerializeField]
    protected Canvas tooltip = null;
    public Canvas Tooltip { get { return tooltip; } }
    private Canvas tooltipInstance = null;

    // Use this for initialization
    void Start()
    {
        Initialize();
    }

    /// <summary>
    /// Applies the effect of the talent
    /// </summary>
    public abstract void Learn();

    /// <summary>
    /// Removes the effect
    /// </summary>
    public abstract void Unlearn();

    /// <summary>
    /// Callback function for toggle ui elements
    /// </summary>
    /// <param name="isLearned">determines if talent was chosen by the user</param>
    public void SetState(bool isLearned)
    {
        if (isLearned)
            Learn();
        else
            Unlearn();
    }

    public void ShowTooltip()
    {
        //tooltipInstance.enabled = true;
        tooltipInstance.GetComponent<CanvasGroup>().alpha = 1f;
    }

    public void HideTooltip()
    {
        //tooltipInstance.enabled = false;
        tooltipInstance.GetComponent<CanvasGroup>().alpha = 0f;
    }

    protected virtual void Initialize()
    {
        GetComponent<TalentBook>().AddTalent(this);
        //if there is a tooltip canvas available
        //set the tooltip content
        if (tooltip != null)
        {
            tooltipInstance = Instantiate(tooltip) as Canvas;
            Text title = tooltipInstance.transform.FindChild("Tooltip-Title").GetComponent<Text>();
            title.text = talentName;
            Text descr = tooltipInstance.transform.FindChild("Tooltip-Description").GetComponent<Text>();
            descr.text = description;
        }
    }

}
