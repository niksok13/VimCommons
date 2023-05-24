namespace Core.Runtime.EZTween
{
    public struct EZData
    {
        public readonly EZ Queue;
        
        public readonly float Linear;
        public readonly float Time;
        
        public float Bounce => EZCurve.Bounce(Linear);
        public float QuadIn => EZCurve.QuadIn(Linear);
        public float QuadOut => EZCurve.QuadOut(Linear);
        public float QuadInOut => EZCurve.QuadInOut(Linear);
        public float QuadOutIn => EZCurve.QuadOutIn(Linear);

        public float QuintIn => EZCurve.QuintIn(Linear);
        public float QuintOut => EZCurve.QuintOut(Linear);
        public float QuintInOut => EZCurve.QuintInOut(Linear);
        public float QuintOutIn => EZCurve.QuintOutIn(Linear);

        public float BackIn => EZCurve.BackIn(Linear);
        public float BackOut => EZCurve.BackOut(Linear);
        public float BackInOut => EZCurve.BackInOut(Linear);
        public float BackOutIn => EZCurve.BackOutIn(Linear);

        public EZData(EZ queue, float progress, float time)
        {
            Queue = queue;
            Linear = progress;
            Time = time;
        }

    }
}