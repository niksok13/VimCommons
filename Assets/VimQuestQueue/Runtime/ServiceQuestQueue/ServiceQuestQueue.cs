using System;
using System.Linq;
using VimAnalytics.Runtime.ServiceAnalytics;
using VimCamera.Runtime.ServiceCamera;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.EZTween;
using VimCore.Runtime.MVVM;
using VimCore.Runtime.Utils;
using VimQuestQueue.Runtime.QuestArrows;
using VimQuestQueue.Runtime.UIQuestQueue;

namespace VimQuestQueue.Runtime.ServiceQuestQueue
{
    public class ServiceQuestQueue : ModelBehaviour, IQuestQueue
    {
        private static readonly ServiceContainer<IQuestQueue> Container = Locator.Single<IQuestQueue>();

        private static INavigationPointerSystem NavigationPointerSystem => Locator.Resolve<INavigationPointerSystem>();
        private static ICamera Camera => Locator.Resolve<ICamera>();
        private static IAnalytics Analytics => Locator.Resolve<IAnalytics>();
        private static IUIQuestQueue UIQuestQueue => Locator.Resolve<IUIQuestQueue>();

        private AQuest[] _quests;
        private AQuest _current;
        private EZ _ez;

        public ObservableData<int> QuestProgress { get; } = new();

        private void Awake()
        {
            Container.Attach(this);
            _quests = GetComponentsInChildren<AQuest>();
            _quests = _quests.OrderBy(i => i.Transform.GetSiblingIndex()).ToArray();
            QuestProgress.ConnectPref("QuestProgress");
            InitQuest();
            LoopUtil.PreUpdate += Tick;
        }

        private void OnDestroy()
        {
            UIQuestQueue.Visible.Value = false;

            Container.Detach(this);
            LoopUtil.PreUpdate += Tick;
        }

        private void InitQuest(EZData ez = default)
        {
            if (QuestProgress.Value >= _quests.Length)
            {
                Destroy(gameObject);
                return;
            }
          
            _current = _quests.ElementAt(QuestProgress.Value);
            if (_current.Camera)
                Camera.Look(_current.Camera);
            UIQuestQueue.ClaimVisible.Value = false;
            UIQuestQueue.Visible.Value = _current.showWindow;
            UIQuestQueue.Title.Value = _current.message;
            UIQuestQueue.Icon.Value = _current.icon;
            UIQuestQueue.Bounce.Invoke();
            UIQuestQueue.ProgressVisible.Value = _current switch
            {
                AQuestCount => true,
                _ => false
            };
            _current.Enter();
        }

        private void Tick()
        {
            if (!_current) return;
            switch (_current)
            {
                case AQuestCount quest:
                    var newProgress =  quest.Current * 1f / quest.Target;
                    UIQuestQueue.Progress.Value = newProgress;
                    UIQuestQueue.ProgressLabel.Value = $"{quest.Current}/{quest.Target}";
                    if (Math.Abs(UIQuestQueue.Progress.Value - newProgress) < float.Epsilon) break;
                    UIQuestQueue.Bounce.Invoke();
                    break;
            }

            if (_current.Done)
            {
                UIQuestQueue.ClaimVisible.Value = true;
                NavigationPointerSystem.Remove(_current.arrowTargets);
                if (_current.autoClaim) 
                    CompleteQuest(true);
            }
            else
            {
                UIQuestQueue.ClaimVisible.Value = false;
                NavigationPointerSystem.Add(_current.arrowTargets);
            }
        }

        public void CompleteQuest(bool auto = false)
        {
            UIQuestQueue.Visible.Value = false;
            _current.Exit();
            Analytics.QuestCompleted(QuestProgress.Value, _current.name);
            QuestProgress.Value += 1;
            _current = null;
            if (auto)
            {
                InitQuest();
                return;
            }
            EZ.Spawn().Delay(1).Call(InitQuest);
        }

        public void LookTarget()
        {
            if(_current.Camera)
                Camera.Look(_current.Camera);
        }

        public void PushEvent<T>(T payload)
        {
            var handler = _current as IQuestEventHandler<T>;
            handler?.PushEvent(payload);
        }
    }
}