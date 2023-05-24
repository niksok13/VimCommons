using System;
using System.Collections.Generic;

namespace Core.Runtime.EZTween
{
    internal static class EZStepPool
    {
        private static readonly Queue<EZStep> StepPool = new(127);

        public static EZStep Get(Action<EZData> action, float duration)
        {
            if (StepPool.TryDequeue(out var result))
            {
                result.Duration = duration;
                result.Action = action;
                return result;
            }

            return new EZStep(action, duration);
        }

        public static void Remove(EZStep step)
        {
            StepPool.Enqueue(step);
            
        }
    }
}