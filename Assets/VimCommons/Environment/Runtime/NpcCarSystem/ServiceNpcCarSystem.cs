using UnityEngine;
using UnityEngine.Splines;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.Pooling;
using VimCore.Runtime.Utils;
using Random = UnityEngine.Random;

namespace VimCommons.Environment.Runtime.NpcCarSystem
{
    public class ServiceNpcCarSystem : MonoBehaviour
    {
        public NpcCar prefab;
        public int amount;

        private PrefabPool<NpcCar> _pool;
        private PrefabPool<NpcCar> Pool => _pool ??= PrefabPool<NpcCar>.Instance(prefab);

        private static readonly Filter<NpcCar> Cars = Locator.Filter<NpcCar>();

        private SplineContainer[] _splines;
        public SplineContainer[] Splines => _splines ??= GetComponentsInChildren<SplineContainer>();         

        private float _timer;


        private void LateUpdate()
        {
            TickSpawner();
            TickUpdate();
        }

        private void TickUpdate()
        {
            foreach (var car in Cars) 
                car.Tick();
        }

        private void TickSpawner()
        {
            if (Cars.Count >= amount) return;
            _timer -= Time.deltaTime;
            if (_timer > 0) return;
            _timer = Random.Range(3, 5);
            Spawn();
        }

        private void Spawn()
        {
            var spline = Splines.GetRandomItem();
            var car = Pool.Spawn();
            car.Init(Pool, spline);
        }

        private void OnDestroy()
        {
            foreach (var car in Cars) 
                Pool.Remove(car);
        }
    }
}