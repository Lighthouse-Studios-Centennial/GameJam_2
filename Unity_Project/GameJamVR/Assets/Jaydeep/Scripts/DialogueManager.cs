using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Dialogues")]
    [SerializeField] private List<DialogueSO> DialoguesList;

    private int currentDialogueIndex = 0;
    private DialogueSO currentDialogueSO;

    private void Start()
    {
        NextDialogue();
    }

    public void NextDialogue()
    {
        SetDialogue();
        ShowDialogue();
    }

    public void SetDialogue()
    {
        currentDialogueIndex = (currentDialogueIndex + 1) % DialoguesList.Count;
        if (currentDialogueIndex == DialoguesList.Count - 1) currentDialogueIndex = 1;
        currentDialogueSO = DialoguesList[currentDialogueIndex];
    }

    public void ShowDialogue()
    {
        var clues = currentDialogueSO.GetDialogues();

        if (clues.Count == 0) return;

        //titleText.text = currentDialogueSO.GetTitle();
        dialogueText.text = clues[0];
    }
}
