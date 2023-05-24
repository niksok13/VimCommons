using Commons.Camera.Runtime.ServiceCamera;
using UnityEngine;

namespace Commons.QuestQueue.Runtime.ServiceQuestQueue
{
    public abstract class AQuest: MonoBehaviour
    {
        private Transform _transform;
        public Transform Transform => _transform ??= transform;

        public bool showWindow = true;
        public string message;
        public Sprite icon;
        public Transform[] arrowTargets;
        public int reward;
        public bool autoClaim;
        
        public CameraState Camera { get; private set; }

        private void Awake()
        {
            if (gameObject.TryGetComponent<CameraState>(out var state))
                Camera = state;
        }

        public abstract bool Done { get; }

        public virtual void Enter() => print($"QuestQueue Enter {GetType().Name}");

        public virtual void Exit() => print($"QuestQueue Exit {GetType().Name}");
    }
}