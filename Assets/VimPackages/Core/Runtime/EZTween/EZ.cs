using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace VimPackages.Core.Runtime.EZTween
{
    public class EZ
    {
        private readonly Queue<EZStep> _queue = new(13);
        private float _stepOrigin;
        private bool _isLooped;
        private bool _run;

        public static EZ Spawn() => new();


        private async void Run()
        {
            if (_run) return;
            _run = true;
            _stepOrigin = Time.realtimeSinceStartup;
            while (_run)
            {
                await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);
                try
                {
                    Tick();
                }
                catch (Exception e)
                {
                    Clear();
                    Debug.LogWarning($"EZ interrupted. {e}");
                }
            }
        }

        public void Tick()
        {
            while (_queue.TryPeek(out var current))
            {
                var stepTime = Time.realtimeSinceStartup - _stepOrigin;
                
                if (stepTime < current.Duration)
                {
                    var progress = stepTime / current.Duration;
                    current.Action?.Invoke(new EZData(this, progress, stepTime));
                    return;
                }
                
                current.Action?.Invoke(new EZData(this, 1, current.Duration));

                _stepOrigin += current.Duration;
                GoNext();
            }
            _run = false;
        }

        private void GoNext()
        {
            if(_queue.TryDequeue(out var last))
                if (_isLooped)
                    _queue.Enqueue(last);
                else
                    EZStepPool.Remove(last);
        }

        public void Clear()
        {
            _queue.Clear();
            _isLooped = false;
            _run = false;
        }

        public void Forward()
        {
            while (_queue.TryDequeue(out var task)) 
                task.Action?.Invoke(new EZData(this, 1, task.Duration));
            Clear();
        }

        public EZ Tween(float duration, Action<EZData> action)
        {
            var step = EZStepPool.Get(action, duration);
            _queue.Enqueue(step);
            Run();
            return this;
        }
        
        public EZ Tween(Action<EZData> action) => Tween(0.3f, action);
        
        public EZ Call(Action<EZData> action) => Tween(0, action);
        
        public EZ Delay(float duration = 0.1f) => Tween(duration, null);
        
        public EZ Wait(Func<bool> func)
        {
            return Tween(float.MaxValue, ez =>
            {
                if (!func.Invoke()) return;
                _stepOrigin = Time.realtimeSinceStartup;
                GoNext();
            });
        }
        public EZ Loop() => Call(_=>_isLooped = true);
        public void Unloop() => _isLooped = false;
    }
}