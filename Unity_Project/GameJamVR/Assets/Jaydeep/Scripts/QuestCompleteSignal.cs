using UnityEngine;

public class QuestCompleteSignal : MonoBehaviour
{
    [SerializeField] private QuestEnum questCompleteType;
    [SerializeField] private QuestManager questManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out var controller))
        {
            questManager.CheckQuestCompletion(questCompleteType);
        }
    }
}
