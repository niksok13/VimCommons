using UnityEngine;
using VimCore.Runtime.EZTween;
using VimCore.Runtime.Utils;

namespace VimCommons.Stacking.Runtime
{
    public class ModelStackable: MonoBehaviour
    {
        public float height = 0.5f;

        private Transform _transform;
        public Transform Transform => _transform ??= transform;

        public StackableDefinition Definition { get; set; }
        public int Weight => Definition.weight;

        public void Build(StackableDefinition definition)
        {
            Definition = definition;
            
            Transform.localScale = Vector3.one;
            Transform.rotation = Quaternion.identity;
        }

        public void Init(Vector3 posSrc, Vector3 posDest)
        {
            Transform.position = posSrc;

            EZ.Spawn().Tween(0.5f, ez =>
            {
                var t = ez.Linear;
                Transform.localPosition = Helper.LerpParabolic(posSrc, posDest, t, 2);
                Transform.localScale = Vector3.one * ez.BackOut;
            });
        }

        public void Init(Transform spawnPoint)
        {
            Transform.position = spawnPoint.position;
            Transform.rotation = spawnPoint.rotation;
        }
        
        public void TweenRemove(Vector3 posTo)
        {
            var posFrom = Transform.localPosition;
            EZ.Spawn().Tween(ez =>
            {
                Transform.localScale = Vector3.one * (1 - ez.BackIn / 2);
                Transform.localPosition = Helper.LerpParabolic(posFrom, posTo, ez.QuadIn);
            }).Call(_ => Remove());
        }
        
        public void Remove() => Definition.Remove(this);
    }
}