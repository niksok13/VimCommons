using Commons.QuestQueue.Runtime.ServiceQuestQueue;
using Core.Runtime.DependencyManagement;
using Core.Runtime.MVVM;
using Core.Runtime.MVVM.ViewModels.Input;
using UnityEngine;

namespace Commons.QuestQueue.Runtime.UIQuestQueue
{
    public class ModelUIQuestQueue : ModelBehaviour, IUIQuestQueue
    {
        private static readonly ServiceContainer<IUIQuestQueue> Container = Locator.Single<IUIQuestQueue>();

        private static IQuestQueue QuestQueue => Locator.Resolve<IQuestQueue>();

        public ObservableData<bool> Visible { get; } = new();
        public ObservableData<string> Title { get; } = new();
        public ObservableData<Sprite> Icon { get; } = new();
        public ObservableData<bool> ProgressVisible { get; } = new();
        public ObservableData<string> ProgressLabel { get; } = new();
        public ObservableData<float> Progress { get; } = new();
        public ObservableData<bool> ClaimVisible { get; } = new();
        public ObservableChannel Bounce { get; } = new();
        
        private void Awake() => Container.Attach(this);

        private void OnDestroy() => Container.Detach(this);


        public void BtnFind(SignalClick _) => QuestQueue.LookTarget();

        public void BtnClaim(SignalClick _) => QuestQueue.CompleteQuest();
    }
}