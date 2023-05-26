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

        public bool Ready { get; private set; }
        
        
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
            }).Call(_ =>
            {
                Ready = true;
            });

        }


        public void Init(Vector3 posSrc)
        {
            Transform.position = posSrc;
            Ready = true;
        }
        
        public void Pick()
        {
            Ready = false;
        }

        public void Remove()
        {
            Pick();
            Definition.Remove(this);
        }
        
        public void TweenRemove(Vector3 posTo)
        {
            var posFrom = Transform.localPosition;
            EZ.Spawn().Tween(ez =>
            {
                Transform.localScale = Vector3.one * (1 - ez.BackIn / 2);
                Transform.localPosition = Helper.LerpParabolic(posFrom, posTo, ez.QuadIn);
            }).Call(Remove);
        }

        private void Remove(EZData ez) => Remove();
    }
}