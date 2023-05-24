using UnityEngine;

namespace VimCore.Runtime.EZTween.Commons
{
    public class EZLoop : MonoBehaviour
    {
        [SerializeField] private float duration = 1;
        [SerializeField] private AnimationCurve scaleX = AnimationCurve.Constant(0,1,1);
        [SerializeField] private AnimationCurve scaleY = AnimationCurve.Constant(0,1,1);
        [SerializeField] private AnimationCurve scaleZ = AnimationCurve.Constant(0,1,1);
        [SerializeField] private AnimationCurve rotX = AnimationCurve.Constant(0,1,0);
        [SerializeField] private AnimationCurve rotY = AnimationCurve.Constant(0,1,0);
        [SerializeField] private AnimationCurve rotZ = AnimationCurve.Constant(0,1,0);
        private EZ _ez;

        private void OnEnable()
        {
            _ez?.Clear();
            _ez = EZ.Spawn().Loop().Tween(duration, Loop);
        }

        private void OnDisable() => _ez?.Clear();

        private void Loop(EZData ez)
        {
            var sx = scaleX.Evaluate(ez.Linear);
            var sy = scaleY.Evaluate(ez.Linear);
            var sz = scaleZ.Evaluate(ez.Linear);
            transform.localScale = new Vector3(sx, sy, sz);
            var rx = rotX.Evaluate(ez.Linear);
            var ry = rotY.Evaluate(ez.Linear);
            var rz = rotZ.Evaluate(ez.Linear);
            transform.localRotation = Quaternion.Euler(rx*360, ry*360, rz*360);
        }
    }
}
