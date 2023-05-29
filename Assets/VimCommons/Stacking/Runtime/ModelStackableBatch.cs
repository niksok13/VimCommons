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
            Amount.Value += 1;
            Content.Push(stackable);

            var posFrom = stackable.Transform.position;
            var rotFrom = stackable.Transform.eulerAngles;
            var posTo = Transform.position + Vector3.up * Count * stackable.height;
            var rotTo = Transform.eulerAngles + Vector3.up * Random.Range(-5, 5);
            
            EZ.Spawn().Tween(ez =>
            {
                stackable.Transform.position = Vector3.Lerp(posFrom, posTo, ez.QuadIn);
                stackable.Transform.eulerAngles = Vector3.Lerp(rotFrom, rotTo, ez.QuadIn);
            });
        }

        public void OnStack(SignalStackInteract signal)
        {
            var stack = signal.Stack;
            if (Content.TryPeek(out var stackable))
            {
                if (stack.Push(stackable))
                {
                    Amount.Value -= 1;
                    Content.Pop();
                }
            }
        }
    }
}