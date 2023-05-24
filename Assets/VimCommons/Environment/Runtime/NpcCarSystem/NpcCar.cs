using UnityEngine;
using UnityEngine.Splines;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.MVVM;
using VimCore.Runtime.Pooling;
using VimCore.Runtime.Utils;

namespace VimCommons.Environment.Runtime.NpcCarSystem
{
    public class NpcCar: ModelBehaviour
    {
        private const float speed = 10;

        public Transform[] visualVariants;
        
        private static readonly Filter<NpcCar> Filter = Locator.Filter<NpcCar>();

        private void OnEnable() => Filter.Add(this);
        private void OnDisable() => Filter.Remove(this);

        public PrefabPool<NpcCar> Pool { get; set; }
        public SplineContainer Path { get; set; }
        public float Length { get; set; }

        public float Progress { get; set; }
        private ObservableData<bool> IsRide { get; } = new();

        private void Awake()
        {
            var prefab = visualVariants.GetRandomItem();
            var model = Instantiate(prefab, transform);
            model.localPosition = Vector3.zero;
            model.localRotation = Quaternion.identity;
            
        }

        public void Init(PrefabPool<NpcCar> pool, SplineContainer spline)
        {
            Pool = pool;
            Path = spline;
            Length = Path.CalculateLength();
            Progress = 0;
            IsRide.Value = true;
        }



        public void Tick()
        {
            if (Progress < 1)
            {
                Progress += speed * Time.deltaTime/Length;
                transform.position = Path.EvaluatePosition(Progress);
                transform.forward = Path.EvaluateTangent(Progress);
                return;
            }
            Remove();
        }

        private void Remove() => Pool.Remove(this);
    }
}