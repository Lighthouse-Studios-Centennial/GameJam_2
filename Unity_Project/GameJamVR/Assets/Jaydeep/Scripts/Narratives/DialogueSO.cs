using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Narratives/Dialogue")]
public class DialogueSO : ScriptableObject
{
    [Header("Dialogue Data")]
    [SerializeField] private QuestEnum Title;
    [SerializeField] private string[] Dialogues;

    public QuestEnum GetQuest()
    {
        return Title;
    }

    public List<string> GetClues()
    {
        return new List<string>(Dialogues);
    }
}
