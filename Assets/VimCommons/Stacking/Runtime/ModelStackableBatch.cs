using System.Collections.Generic;
using UnityEngine;
using VimCore.Runtime.EZTween;
using VimCore.Runtime.MVVM;

namespace VimCommons.Stacking.Runtime
{
    public class ModelStackableBatch: ModelBehaviour
    {
        private Transform _transform;
        public Transform Transform => _transform ??= transform;
        
        public Vector3 size = Vector3.one;
        public Vector2Int table = new(2, 2);
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
            var posFrom = stackable.Transform.position;
            var rotFrom = stackable.Transform.eulerAngles;
            var posTo = Transform.position + Vector3.up + Transform.rotation * GridPos(Count, table.x, table.y);
            var rotTo = Transform.eulerAngles + Vector3.up * Random.Range(-5, 5);

            Content.Push(stackable);
            Amount.Value += 1;

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

            return Vector3.Scale(grid,size);
        }

        public void OnStack(SignalStackInteract signal)
        {
            var stack = signal.Stack;
            if (!Content.TryPeek(out var stackable)) return;
            if (!stack.HaveSpace(stackable.Weight)) return;
            stack.Push(stackable);
            Amount.Value -= 1;
            Content.Pop();
        }
    }
}