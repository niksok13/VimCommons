using System.Collections.Generic;
using UnityEngine;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.EZTween;
using VimCore.Runtime.MVVM;
using VimCore.Runtime.Utils;

namespace VimCommons.Stacking.Runtime
{
    public class ModelStackableBatch: ModelBehaviour
    {
        public int maxAmount = 50;
        public float lootRadius = 2;
        public Vector3 size = new (0.6f, 0.15f, 1.2f);

        private static readonly Filter<ModelStackableBatch> Filter = Locator.Filter<ModelStackableBatch>();
        private void OnEnable() => Filter.Add(this);
        private void OnDisable() => Filter.Remove(this);
        
        private Transform _transform;
        public Transform Transform => _transform ??= transform;
        
        private Stack<ModelStackable> Content { get; } = new();
        public int Count => Content.Count;

        private ObservableData<int> Amount { get; } = new();
        private ObservableChannel Bounce { get; } = new();
        private ObservableData<string> Label { get; } = new();

        private void Awake() => Amount.OnValue += OnAmount;

        private void OnDestroy()
        {
            foreach (var stackable in Content) stackable.Remove();
            Content.Clear();
            Amount.Value = 0;
        }

        private void OnAmount(int obj)
        {
            Label.Value = obj.ToString();
            Bounce.Invoke();
        }


        public void Push(ModelStackable stackable)
        {
            if (Amount.Value >= maxAmount) return;
            Amount.Value += 1;
            Content.Push(stackable);

            var posFrom = stackable.Transform.position;
            var rotFrom = stackable.Transform.eulerAngles;
            var posTo =  Transform.position + Transform.rotation * GridPos(Content.Count, 2, 4);
            var rotTo = Transform.eulerAngles + Vector3.up * Random.Range(-5, 5);
            
            EZ.Spawn().Tween(ez =>
            {
                stackable.Transform.position = Vector3.Lerp(posFrom, posTo, ez.QuadIn);
                stackable.Transform.eulerAngles = Vector3.Lerp(rotFrom, rotTo, ez.QuadIn);
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

            return Vector3.Scale(grid, size);
        }

        public void Tick(StackComponent stack)
        {
            if (Count < 1) return;
            if (!Helper.WithinRadius(Transform, stack.Transform, lootRadius)) return;
            Collect(stack);
        }
        
        public void Collect(StackComponent stack)
        {
            if (Content.TryPop(out var lootable))
            {
                Amount.Value -= 1;
                var from = lootable.Transform.position;
                EZ.Spawn().Tween(ez => {
                    lootable.Transform.localScale = Vector3.one * ez.BackOut;
                    lootable.Transform.position = Helper.LerpParabolic(from, stack.Transform.position + Vector3.up, ez.Linear);
                }).Call(item =>  {
                    lootable.Remove();
                    stack.Push(lootable);
                });
            }
        }
    }
}