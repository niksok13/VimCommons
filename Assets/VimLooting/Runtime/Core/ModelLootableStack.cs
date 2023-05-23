using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.EZTween;
using VimCore.Runtime.MVVM;
using VimCore.Runtime.Utils;
using Random = UnityEngine.Random;

namespace VimLooting.Runtime.Core
{
    public class ModelLootableStack: ModelBehaviour
    {        
        public int maxAmount = 50;
        public float lootRadius = 2;
        public Vector3 size = new(0.6f,0.15f,1.2f);

        public LootableDefinition type;
        private static readonly Filter<ModelLootableStack> Filter = Locator.Filter<ModelLootableStack>();

        private void OnEnable() => Filter.Add(this);
        private void OnDisable() => Filter.Remove(this);
        
        private Transform _transform;
        public Transform Transform => _transform ??= transform;
        
        
        private readonly Stack<Lootable> _stack = new();
        public int Count => _stack.Count;

        private ObservableData<int> Amount { get; } = new();
        private ObservableChannel Bounce { get; } = new();
        private ObservableData<string> Label { get; } = new();

        private void Awake()
        {
            Amount.OnValue += OnAmount;
        }

        private void OnDestroy()
        {
            foreach (var lootable in _stack) lootable.Remove();
            _stack.Clear();
            Amount.Value = 0;
        }

        private void OnAmount(int obj)
        {
            Label.Value = obj.ToString();
            Bounce.Invoke();
        }

        public void Append(int amount)
        {
            for (var i = 0; i < amount; i++) 
                AppendItem();
        }

        private void AppendItem()
        {
            Amount.Value += 1;
            var index = _stack.Count;
            if (index >= maxAmount) return;
            var coin = type.Spawn();
            coin.Transform.localPosition = Transform.position + Transform.rotation * GridPos(index, 2, 4);
            coin.Transform.eulerAngles = Transform.eulerAngles + Vector3.up * Random.Range(-5, 5);
            coin.Transform.localScale = Vector3.zero;
            _stack.Push(coin);
            EZ.Spawn().Tween(ez => {
                coin.Transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, ez.BackIn);
            });
        }

        private Vector3 GridPos(int a, int dimB, int dimC)
        {
            var b = a % dimB;
            a /= dimB;
            var c = a % dimC;
            a /= dimC;

            var grid = new Vector3(
                c - 0.5f * (dimC - 1), 
                a, 
                b - 0.5f * (dimB - 1));

            return Vector3.Scale(grid,size);
        }

        public void Tick(LooterComponent looter)
        {
            if (Count < 1) return;
            if (!Helper.WithinRadius(Transform, looter.Transform, lootRadius)) return;
            Collect(looter);
        }
        
        public async void Collect(LooterComponent looter)
        {
            foreach (var lootable in _stack)
            {
                var from = lootable.Transform.position;
                EZ.Spawn().Tween(ez => {
                    lootable.Transform.localScale = Vector3.one * ez.BackOut;
                    lootable.Transform.position = Helper.LerpParabolic(from, looter.Transform.position + Vector3.up, ez.Linear);
                }).Call(item =>  {
                    lootable.Remove();
                });
            }
            _stack.Clear();
            await UniTask.Delay(300, DelayType.UnscaledDeltaTime);
            for (int i = 0; i < Amount.Value; i++) 
                looter.Loot(type);
            Amount.Value = 0;
        }

    }
}