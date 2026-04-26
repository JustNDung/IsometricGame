
using Reward;

namespace Quest
{
    using UnityEngine;
    using System.Collections.Generic;

    public class QuestManager : MonoBehaviour
    {
        public static QuestManager Instance;

        [SerializeField] private List<QuestSO> startQuests;

        private List<QuestInstance> activeQuests =
            new List<QuestInstance>();

        private void Awake()
        {
            Instance = this;

            foreach (var q in startQuests)
                AddQuest(q);
        }

        public void AddQuest(QuestSO quest)
        {
            activeQuests.Add(new QuestInstance(quest));
        }

        public void ProcessEvent(RewardEvent e)
        {
            foreach (var quest in activeQuests)
            {
                bool wasComplete = quest.IsCompleted;

                quest.ProcessEvent(e);

                if (!wasComplete && quest.IsCompleted)
                {
                    CompleteQuest(quest);
                }
            }
        }

        void CompleteQuest(QuestInstance quest)
        {
            RewardSystem.Instance.ProcessEvent(new RewardEvent
            {
                experimentId = "Quest",
                actionId = quest.data.questId,
                success = true
            });

            Debug.Log("Quest Complete: " + quest.data.title);
        }
    }
}