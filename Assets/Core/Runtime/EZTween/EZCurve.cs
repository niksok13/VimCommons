using UnityEngine;

namespace Core.Runtime.EZTween
{
    public static class EZCurve
    {
        //Quad
        public static float QuadIn(float t) => t * t;
        public static float QuadOut(float t) => 1 - (1 - t) * (1 - t);
        public static float QuadInOut(float t) => Mathf.Lerp(QuadIn(t),QuadOut(t),t);
        public static float QuadOutIn(float t) => Mathf.Lerp(QuadIn(t),QuadOut(t),t-1);

        //Quint
        public static float QuintIn(float t) => t * t * t * t * t;
        public static float QuintOut(float t) => 1 - (1 - t) * (1 - t) * (1 - t) * (1 - t) * (1 - t);
        public static float QuintInOut(float t) => Mathf.Lerp(QuintIn(t),QuintOut(t),t);
        public static float QuintOutIn(float t) => Mathf.Lerp(QuintIn(t),QuintOut(t),t-1);

        //Back
        private const float B1 = 1.70158f;
        private const float B2 = 2.70158f;
        public static float BackIn(float t) => B2 * t * t * t - B1 * t * t;
        public static float BackOut(float t) => 1 - BackIn(1 - t);
        public static float BackInOut(float t) => Mathf.Lerp(BackIn(t),BackOut(t),t);
        public static float BackOutIn(float t) => Mathf.Lerp(BackIn(t),BackOut(t),t-1);

        public static float Bounce(float t) => t * (1 - t);

    }
}