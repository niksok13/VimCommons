using System;

namespace VimPackages.Core.Runtime.EZTween
{
    internal class EZStep
    {
        public float Duration;
        public Action<EZData> Action;

        public EZStep(Action<EZData> action, float duration)
        {
            Action = action;
            Duration = duration;
        }
    }
}