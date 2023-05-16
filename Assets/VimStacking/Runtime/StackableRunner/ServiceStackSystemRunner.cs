using UnityEngine;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.Utils;
using VimStacking.Runtime.StackableConverter;
using VimStacking.Runtime.StackableEmitter;

namespace VimStacking.Runtime.StackableRunner
{
    public class ServiceStackSystemRunner : MonoBehaviour
    {
        private static readonly Filter<StackComponent> Stacks = Locator.Filter<StackComponent>();
        private static readonly Filter<ModelStackableEmitter> Emitters = Locator.Filter<ModelStackableEmitter>();
        private static readonly Filter<ModelStackableConverter> Converters = Locator.Filter<ModelStackableConverter>();
        public void Awake()
        {
            LoopUtil.PreUpdate += Tick;
            LoopUtil.PostLateUpdate += LateTick;
        }

        private void OnDestroy()
        {
            LoopUtil.PreUpdate -= Tick;
            LoopUtil.PostLateUpdate -= LateTick;
        }
        
        private static void Tick()
        {
            foreach (var stack in Stacks) stack.TickUpdate();
            foreach (var emitter in Emitters) emitter.TickUpdate();
            foreach (var converter in Converters) converter.TickUpdate();
        }

        private static void LateTick()
        {
            foreach (var stack in Stacks) stack.TickLateUpdate();
        }
    }
}