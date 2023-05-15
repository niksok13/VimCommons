using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;

namespace VimPackages.Core.Runtime.Utils
{
    public static class LoopUtil
    {
        static LoopUtil()
        {
            var loop = PlayerLoop.GetCurrentPlayerLoop();
            loop.subSystemList[1].updateDelegate += InitializationFunc;
            loop.subSystemList[5].updateDelegate += PreUpdateFunc;
            loop.subSystemList[6].updateDelegate += PostUpdateFunc;
            loop.subSystemList[7].updateDelegate += PostLateUpdateFunc;
            PlayerLoop.SetPlayerLoop(loop);
        }
        
        private static float _frameStart;


        private static void InitializationFunc()
        {
            var now = Time.realtimeSinceStartup;
            var delta = now - _frameStart;
            Fps = Mathf.Lerp(Fps, 1f / delta, 0.1f);
            _frameStart = now;
        }


        private static float _lastPreUpdate;
        private static void PreUpdateFunc()
        {
            var realtime = Time.realtimeSinceStartup;
            Delta = Time.timeScale *(realtime - _lastPreUpdate);
            _lastPreUpdate = realtime;
            for (var i = PreUpdateList.Count - 1; i >= 0; i--) 
                PreUpdateList[i].Invoke();
        }

        private static float _lastPostUpdate;
        private static void PostUpdateFunc()
        {
            var realtime = Time.realtimeSinceStartup;
            Delta = Time.timeScale *(realtime - _lastPostUpdate);
            _lastPostUpdate = realtime;
            for (var i = PostUpdateList.Count - 1; i >= 0; i--) 
                PostUpdateList[i].Invoke();
        }

        private static float _lastPostLateUpdate;
        private static void PostLateUpdateFunc()
        {
            var realtime = Time.realtimeSinceStartup;
            Delta = Time.timeScale *(realtime - _lastPostLateUpdate);
            _lastPostLateUpdate = realtime;
            for (var i = PostLateUpdateList.Count - 1; i >= 0; i--) 
                PostLateUpdateList[i].Invoke();
        }
        
        
        public static float Fps { get; private set; }
        public static float FrameTime => Time.realtimeSinceStartup - _frameStart;
        
        public static float Delta { get; private set; }
        
        private static readonly List<Action> PreUpdateList = new();
        private static readonly List<Action> PostUpdateList = new();
        private static readonly List<Action> PostLateUpdateList = new();
        
        
        public static event Action PreUpdate
        {
            add => PreUpdateList.Add(value);
            remove => PreUpdateList.Remove(value);
        }

        public static event Action PostUpdate
        {
            add => PostUpdateList.Add(value);
            remove => PostUpdateList.Remove(value);
        }
        
        public static event Action PostLateUpdate
        {
            add => PostLateUpdateList.Add(value);
            remove => PostLateUpdateList.Remove(value);
        }
    }
}