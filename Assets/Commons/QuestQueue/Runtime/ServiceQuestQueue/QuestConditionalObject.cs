using Core.Runtime.DependencyManagement;
using UnityEngine;

namespace Commons.QuestQueue.Runtime.ServiceQuestQueue
{
    public class QuestConditionalObject: MonoBehaviour
    {
        private static IQuestQueue QuestQueue => Locator.Resolve<IQuestQueue>();
        
        public int destroyStep;

        private void Awake() => QuestQueue.QuestProgress.OnValue+=OnQuest;

        private void OnDestroy() => QuestQueue.QuestProgress.OnValue-=OnQuest;

        private void OnQuest(int obj)
        {
            if (obj < destroyStep) return;
            Destroy(gameObject);
        }
    }
}