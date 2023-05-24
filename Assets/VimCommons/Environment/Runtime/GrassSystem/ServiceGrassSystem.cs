using Core.Runtime.DependencyManagement;
using Core.Runtime.Utils;
using UnityEngine;

namespace Commons.Environment.Runtime.GrassSystem
{
    public class ServiceGrassSystem : MonoBehaviour
    {
        private static readonly int InvokersCount = Shader.PropertyToID("_InvokersCount");
        private static readonly int InvokersArray = Shader.PropertyToID("_InvokersArray");
        
        private readonly Vector4[] _array = new Vector4[20];
        private readonly Vector4 _negInf = Vector4.negativeInfinity;

        private static Filter<GrassEffector> Effectors = Locator.Filter<GrassEffector>();

        private Vector3 _curPos;
        private int _effCount;

        private void Awake() => LoopUtil.PreUpdate += Tick;

        private void OnDestroy() => LoopUtil.PreUpdate -= Tick;

        private void Tick()
        {
            _effCount = Mathf.Min(Effectors.Count, 20);
            var i = 0;
            foreach (var effector in Effectors)
            {
                _curPos = effector.Transform.position;
                _array[i] = new Vector4(_curPos.x, 0, _curPos.z, 0);
                i += 1;
                if (i >= _effCount) break;
            }
            Shader.SetGlobalInt(InvokersCount, _effCount);
            Shader.SetGlobalVectorArray(InvokersArray, _array);
        }
    }
}