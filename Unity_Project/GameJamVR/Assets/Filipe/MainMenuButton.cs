using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class MainMenuButton : MonoBehaviour
{
    XRBaseInteractable interactable;

    [SerializeField] MainMenuController mainMenuController;
    [SerializeField] string title;
    [SerializeField] string description;


    private void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(OnHoverEnter);
        interactable.hoverExited.AddListener(OnHoverExit);
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        mainMenuController.ShowMoreInfo(title, description);

    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        mainMenuController.HideMoreInfo();

    }

}
