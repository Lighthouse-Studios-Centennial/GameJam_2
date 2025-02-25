using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    [Header("Input Action For UI")]
    [SerializeField] private InputActionProperty dialogueInputActionProp;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialoguePanel;

    [Header("Dialogues")]
    [SerializeField] private List<DialogueSO> DialoguesList;

    private int currentDialogueIndex = 0;
    private int currentClueIndex = 0;
    private bool isDialoguesAreOver = false;
    [SerializeField] private DialogueSO currentDialogueSO;

    public void StartDialogue()
    {
        gameObject.SetActive(true);
    }

    public void StopDialogue()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        NextDialogue();
    }

    private void Update()
    {
        if (!isDialoguesAreOver && dialogueInputActionProp.action.triggered)
        {
            dialoguePanel.SetActive(!dialoguePanel.activeSelf);
        }
    }

    public DialogueSO GetCurrentDialogueSO()
    {
        return currentDialogueSO;
    }

    public void NextDialogue()
    {
        SetNextDialogue();
        NextClue();
    }

    public void SetIsDialoguesAreOver(bool value)
    {
        isDialoguesAreOver = value;
    }

    public void SetNextDialogue()
    {
        currentDialogueIndex = (currentDialogueIndex + 1) % DialoguesList.Count;
        if (currentDialogueIndex == DialoguesList.Count - 1) currentDialogueIndex = 1;
        currentDialogueSO = DialoguesList[currentDialogueIndex];
    }

    public void NextClue()
    {
        var clues = currentDialogueSO.GetClues();

        if (clues.Count == 0) return;

        dialogueText.text = clues[currentClueIndex];
        currentClueIndex = (currentClueIndex + 1) % clues.Count;
    }
}
