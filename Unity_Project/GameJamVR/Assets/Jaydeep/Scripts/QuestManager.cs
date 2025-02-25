using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private ParticleSystem conffettiParticles;

    private bool isKingdomVisited;
    private bool isEnslavementVisited;
    private bool isNewLandVisited;
    private bool isReclaimedVisited;
    private bool isWarriorVisited;

    private bool isQuestMode;

    public void DiscoveryMode()
    {
        dialogueManager.StartDialogue();
        gameObject.SetActive(true);

        isQuestMode = true;
    }

    public void ExplorationMode()
    {
        dialogueManager.StopDialogue();
        gameObject.SetActive(false);

        isQuestMode = false;
    }

    public void CheckQuestCompletion(QuestEnum questEnum)
    {
        if (!isQuestMode) return;

        var currentQuest = dialogueManager.GetCurrentDialogueSO().GetQuest();

        if (currentQuest == questEnum) // Check if any questEnum is matching with the current quest
        {
            switch (questEnum)
            {
                case QuestEnum.Kingdom:
                    SetKingdomVisited(true);
                    break;
                case QuestEnum.Enslavement:
                    SetEnslavementVisited(true);
                    break;
                case QuestEnum.NewLand:
                    SetNewLandVisited(true);
                    break;
                case QuestEnum.Reclaimation:
                    SetReclaimedVisited(true);
                    break;
                case QuestEnum.Warriors:
                    SetWarriorVisited(true);
                    break;
            }

            dialogueManager.NextDialogue();
        }
    }

    public void SetKingdomVisited(bool value)
    {
        isKingdomVisited = value;
        CheckIfAllVisited();
    }

    public void SetEnslavementVisited(bool value)
    {
        isEnslavementVisited = value;
        CheckIfAllVisited();
    }

    public void SetNewLandVisited(bool value)
    {
        isNewLandVisited = value;
        CheckIfAllVisited();
    }

    public void SetReclaimedVisited(bool value)
    {
        isReclaimedVisited = value;
        CheckIfAllVisited();
    }

    public void SetWarriorVisited(bool value)
    {
        isWarriorVisited = value;
        CheckIfAllVisited();
    }

    private void CheckIfAllVisited()
    {
        if (isKingdomVisited && isEnslavementVisited && isNewLandVisited && isReclaimedVisited && isWarriorVisited)
        {
            Debug.Log("All quests are visited");
            conffettiParticles.Play();
            dialogueManager.SetIsDialoguesAreOver(true);
        }
    }
}