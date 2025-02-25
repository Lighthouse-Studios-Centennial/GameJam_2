using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moreInfoTitle;
    [SerializeField] TextMeshProUGUI moreInfoDescription;
    [SerializeField] Animator moreInfoAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    
    public void ShowMoreInfo(string Title, string Description)
    {
        moreInfoTitle.text = Title;
        moreInfoDescription.text = Description;
        moreInfoAnimator.SetBool("Highlighting", true);
    }
    public void HideMoreInfo()
    {
        moreInfoAnimator.SetBool("Highlighting", false);
    }


}
