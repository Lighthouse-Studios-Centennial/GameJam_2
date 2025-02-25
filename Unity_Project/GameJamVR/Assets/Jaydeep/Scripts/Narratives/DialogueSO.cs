using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Narratives/Dialogue")]
public class DialogueSO : ScriptableObject
{
    [Header("Dialogue Data")]
    [SerializeField] private string Title;
    [SerializeField] private string[] Dialogues;

    public string GetTitle()
    {
        return Title;
    }

    public List<string> GetDialogues()
    {
        return new List<string>(Dialogues);
    }
}
